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
	public class BillsController : ApiController
	{
		private Authorizer auth;

        public HttpResponseMessage Get(int? count, int? project, int? department, DateTime startDate, DateTime endDate, int? lastId)
        {
            BillsFilter billsFilter = new BillsFilter();
            billsFilter.Project = project;
            billsFilter.Department = department;
            billsFilter.StartDate = startDate;
            billsFilter.EndDate = endDate;
            billsFilter.LastId = lastId;
            string errMsg = "";
            IList<Model.Bill> listBill = BLL.BillManager.GetBills(count, billsFilter, out errMsg);
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
                    bills = listBill
                };
                json = JsonConvert.SerializeObject(msg2);
            }
            return new HttpResponseMessage(status)
            {
                Content = new StringContent(json, System.Text.Encoding.GetEncoding("UTF-8"), "application/json")
            };
        }

        public HttpResponseMessage Get(DateTime startDate, DateTime endDate)
        {
            string errMsg = "";
            IList<Model.Bill> listBill = BLL.BillManager.GetBillsByTime(startDate, endDate, out errMsg);
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
                    bills = listBill
                };
                json = JsonConvert.SerializeObject(msg2);
            }
            return new HttpResponseMessage(status)
            {
                Content = new StringContent(json, System.Text.Encoding.GetEncoding("UTF-8"), "application/json")
            };
        }

        public HttpResponseMessage Get(int id, int car)
        {
            string errMsg = "";
            IList<Model.Bill> listBill = BLL.BillManager.GetLastOneBillByCar(id, car, out errMsg);
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
                    bills = listBill
                };
                json = JsonConvert.SerializeObject(msg2);
            }
            return new HttpResponseMessage(status)
            {
                Content = new StringContent(json, System.Text.Encoding.GetEncoding("UTF-8"), "application/json")
            };
        }

        public HttpResponseMessage Get(int id)
        {
            string errMsg = "";
            Model.Bill bill = BLL.BillManager.GetBillById(id, out errMsg);
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
                    bill = bill
                };
                json = JsonConvert.SerializeObject(msg2);
            }
            return new HttpResponseMessage(status)
            {
                Content = new StringContent(json, System.Text.Encoding.GetEncoding("UTF-8"), "application/json")
            };
        }

		public HttpResponseMessage Post(BillWrap billWrap)
		{
			string errMsg = "";
			string errMsgForChangeset = "";
			bool isChangesetError = false;
			Bill model = billWrap.Bill;
			Changeset changeset = new Changeset();
			try
			{
				string token = base.ControllerContext.Request.Headers.GetValues("Token").First<string>();
				bool isChecked = TokenManage.CheckAuthorizer(token, PowerStatusCode.Scanner, out this.auth, out errMsg);
				if (isChecked)
				{
					if (this.tryValidate(model, out errMsg))
					{
						if (this.tryPost(ref model, out errMsg))
						{
							if (!ChangesetManager.tryFetch(this.auth.Sync, ref changeset, out errMsgForChangeset))
							{
								isChangesetError = true;
							}
						}
					}
				}
			}
			catch (System.Exception ex)
			{
				errMsg = ex.Message;
			}
			HttpStatusCode status;
			string json;
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
					Bill = model,
					Changeset = changeset,
					IsChangesetError = isChangesetError,
					ErrMsgForChangeset = errMsgForChangeset
				};
				json = JsonConvert.SerializeObject(msg2);
			}
			return new HttpResponseMessage(status)
			{
				Content = new StringContent(json, System.Text.Encoding.GetEncoding("UTF-8"), "application/json")
			};
		}

        public HttpResponseMessage Put(int id, BillWrap billWrap)
        {
            string errMsg = "";
            string errMsgForChangeset = "";
            bool isChangesetError = false;
            Bill model = billWrap.Bill;
            model.Id = id;
            Model.Changeset changeset = new Model.Changeset();
            try
            {
                string token = this.ControllerContext.Request.Headers.GetValues("Token").First();
                bool isChecked = TokenManage.CheckAuthorizer(token, PowerStatusCode.Loser, out auth, out errMsg);
                if (isChecked)
                {
                    if (tryValidate(model, out errMsg))
                    {
                        if (this.tryPut(ref model, out errMsg))
                        {
                            if (!ChangesetManager.tryFetch(auth.Sync, ref changeset, out errMsgForChangeset))
                            {
                                isChangesetError = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            var status = HttpStatusCode.OK;
            string json = "";
            if (errMsg.Length > 0)
            {
                status = (HttpStatusCode)422;
                var msg = new { errors = new { ServerSideError = errMsg } };
                json = JsonConvert.SerializeObject(msg);
            }
            else
            {
                status = HttpStatusCode.OK;
                var msg = new { Bill = model, Changeset = changeset, IsChangesetError = isChangesetError, ErrMsgForChangeset = errMsgForChangeset };
                json = JsonConvert.SerializeObject(msg);
            }
            return new HttpResponseMessage(status)
            {
                Content = new StringContent(json, System.Text.Encoding.GetEncoding("UTF-8"), "application/json")
            };
        }

		private bool tryValidate(Bill model, out string errMsg)
		{
			errMsg = "";
			bool re = true;
			double arg_10_0 = model.Volume;
			bool arg_64_0;
			if (model.Volume != 0.0)
			{
				double arg_28_0 = model.Mileage;
				if (model.Mileage != 0.0)
				{
					arg_64_0 = (model.DriverName.Length <= 20);
					goto IL_64;
				}
			}
			arg_64_0 = false;
			IL_64:
			if (!arg_64_0)
			{
				errMsg = "输入数据不合法";
				re = false;
			}
			if (re && model.Modifier != this.auth.UserId)
			{
				errMsg = "登录信息异常，请刷新浏览器以重启应用(APP请退出应用后重新打开)";
				re = false;
			}
			return re;
		}

		private bool tryPost(ref Bill model, out string errMsg)
		{
			errMsg = "";
			bool re = false;
            model.Oiler = this.auth.UserId;
			model.Time = System.DateTime.Now;
			model.Creater = this.auth.UserId;
			model.CreatedDate = System.DateTime.Now;
			model.Modifier = this.auth.UserId;
			model.ModifiedDate = System.DateTime.Now;
			if (BillManager.AddBill(ref model, out errMsg))
			{
				re = true;
			}
			return re;
		}

        private bool tryPut(ref Model.Bill model, out string errMsg)
        {
            errMsg = "";
            bool re = false;
            model.Modifier = this.auth.UserId;
            model.ModifiedDate = DateTime.Now;
            if (BillManager.ModifyBillById(ref model, out errMsg))
            {
                re = true;
            }
            return re;
        }
	}
}
