using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Collections.Generic;
using BLL;
using Model;
using Newtonsoft.Json;
using System.Web;
using Utility;

namespace API.Controllers
{
	public class ExportsController : ApiController
	{
        public HttpResponseMessage Get(int? count, int? project, int? department, DateTime startDate, DateTime endDate, int? lastId, string name)
        {
            BillsFilter billsFilter = new BillsFilter();
            billsFilter.Project = project;
            billsFilter.Department = department;
            billsFilter.StartDate = startDate;
            billsFilter.EndDate = endDate;
            billsFilter.LastId = lastId;
            string errMsg = "";
            IList<Model.Export> listExport = new List<Model.Export>();
            IList<Model.BillPrinter> listBill = BLL.BillManager.GetBillsForExport(count, billsFilter, out errMsg);
            if (listBill == null)
            {
                errMsg = "数据量过大,请适当缩小时间范围！";
            }
            else if (lastId == 0 && listBill.Count == 0)
            {
                errMsg = "指定条件下没有找到加油单！";
            }
            else
            {
                try
                {
                    Model.Export export = new Model.Export();
                    int newLastId = (int)lastId;
                    if (listBill.Count > 0)
                    {
                        newLastId = listBill[listBill.Count - 1].Id;
                        StringBuilder sb = GetBillTextForExport(listBill, (lastId > 0 ? false : true));
                        byte[] data = Encoding.GetEncoding("gb2312").GetBytes(sb.ToString());
                        //byte[] data = new UTF8Encoding().GetBytes(sb.ToString());
                        string filePath = HttpContext.Current.Server.MapPath("../download//" + name + ".csv");
                        Function.BytesToFile(data, filePath);
                    }
                    export.Id = 1;
                    export.Length = listBill.Count;
                    export.LastId = newLastId;
                    listExport.Add(export);
                }
                catch (Exception ex)
                {
                    errMsg = ex.Message;
                }
            }
            HttpStatusCode status;
            string json;
            //errMsg = "网络繁忙，请稍稍再度";
            if (errMsg.Length > 0)
            {
                status = (HttpStatusCode)422;
                var msg = new
                {
                    errors = new
                    {
                        ServerSideError = errMsg
                    }
                };
                json = JsonConvert.SerializeObject(msg);
            }
            else
            {
                status = HttpStatusCode.OK;
                var msg2 = new
                {
                    exports = listExport
                };
                json = JsonConvert.SerializeObject(msg2);
            }
            return new HttpResponseMessage(status)
            {
                Content = new StringContent(json, System.Text.Encoding.GetEncoding("UTF-8"), "application/json")
            };
        }

        public HttpResponseMessage Get(int? project, int? department, DateTime startDate, DateTime endDate)
        {
            BillsFilter billsFilter = new BillsFilter();
            billsFilter.Project = project;
            billsFilter.Department = department;
            billsFilter.StartDate = startDate;
            billsFilter.EndDate = endDate;
            billsFilter.LastId = 0;
            string errMsg = "";
            IList<Model.Export> listExport = new List<Model.Export>();
            int billsCount = BLL.BillManager.GetBillsCountForExport(billsFilter, out errMsg);
            if (billsCount == 0)
            {
                errMsg = "指定条件下没有找到加油单！";
            }
            else if (billsCount > 0)
            {
                try
                {
                    Model.Export export = new Model.Export();
                    export.Id = 1;
                    export.Length = 0;
                    export.Total = billsCount;
                    listExport.Add(export);
                }
                catch (Exception ex)
                {
                    errMsg = ex.Message;
                }
            }
            HttpStatusCode status;
            string json;
            //errMsg = "网络繁忙，请稍稍再度";
            if (errMsg.Length > 0)
            {
                status = (HttpStatusCode)422;
                var msg = new
                {
                    errors = new
                    {
                        ServerSideError = errMsg
                    }
                };
                json = JsonConvert.SerializeObject(msg);
            }
            else
            {
                status = HttpStatusCode.OK;
                var msg2 = new
                {
                    exports = listExport
                };
                json = JsonConvert.SerializeObject(msg2);
            }
            return new HttpResponseMessage(status)
            {
                Content = new StringContent(json, System.Text.Encoding.GetEncoding("UTF-8"), "application/json")
            };
        }

        private StringBuilder GetBillTextForExport(IList<Model.BillPrinter> listBill,bool isFirst)
        {
            StringBuilder sb = new StringBuilder();
            BillPrinter tempBill;
            if (isFirst)
            {
                sb.Append("单号");
                sb.Append(",");
                sb.Append("车辆编号");
                sb.Append(",");
                sb.Append("VIN");
                sb.Append(",");
                sb.Append("所属项目");
                sb.Append(",");
                sb.Append("使用部门");
                sb.Append(",");
                sb.Append("油品");
                sb.Append(",");
                sb.Append("加油量");
                sb.Append(",");
                sb.Append("里程数");
                //sb.Append(",");
                //sb.Append("驾驶员");
                sb.Append(",");
                sb.Append("油耗");
                sb.Append(",");
                sb.Append("加油工");
                sb.Append(",");
                sb.Append("使用人");
                sb.Append(",");
                sb.Append("申请人");
                sb.Append(",");
                sb.Append("加油时间");
                sb.Append(",");
                sb.Append("是否已打印");
                sb.Append(",");
                sb.Append("是否修正过");
                sb.Append("\r\n");
            }
            for (int i = 0, lenI = listBill.Count; i < lenI; i++)
            {
                tempBill = listBill[i];
                sb.Append(tempBill.Id);
                sb.Append(",");
                sb.Append(tempBill.CarNumber);
                sb.Append(",");
                sb.Append(tempBill.CarVin);
                sb.Append(",");
                sb.Append(tempBill.ProjectName);
                sb.Append(",");
                sb.Append(tempBill.DepartmentName);
                sb.Append(",");
                sb.Append(tempBill.OilName);
                sb.Append(",");
                sb.Append(tempBill.Volume);
                sb.Append(",");
                sb.Append(tempBill.Mileage);
                //sb.Append(",");
                //sb.Append(tempBill.DriverName);
                sb.Append(",");
                sb.Append(tempBill.Rate);
                sb.Append(",");
                sb.Append(tempBill.OilerName);
                sb.Append(",");
                sb.Append(tempBill.UserName);
                sb.Append(",");
                sb.Append(tempBill.ApplicantName);
                sb.Append(",");
                sb.Append(tempBill.Time);
                sb.Append(",");
                sb.Append(tempBill.IsPrinted ? "已打印" : "未打印");
                sb.Append(",");
                sb.Append(tempBill.IsLost ? "是" : "否");
                sb.Append("\r\n");
            }
            return sb;
        }
    }
}
