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

namespace API.Controllers
{
	public class ReportsController : ApiController
	{
        public HttpResponseMessage Get(int? project, int? department, DateTime startDate, DateTime endDate)
        {
            ReportsFilter reportsFilter = new ReportsFilter();
            reportsFilter.Project = project;
            reportsFilter.Department = department;
            reportsFilter.StartDate = startDate;
            reportsFilter.EndDate = endDate;
            string errMsg = "";
            IList<Model.Report> listReport = BLL.BillManager.GetReports(reportsFilter, out errMsg);
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
                    reports = listReport
                };
                json = JsonConvert.SerializeObject(msg2);
            }
            return new HttpResponseMessage(status)
            {
                Content = new StringContent(json, System.Text.Encoding.GetEncoding("UTF-8"), "application/json")
            };
        }
	}
}
