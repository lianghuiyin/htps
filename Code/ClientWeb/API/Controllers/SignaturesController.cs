using BLL;
using Model;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace API.Controllers
{
	public class SignaturesController : ApiController
	{
		private Authorizer auth;

		public HttpResponseMessage Post(SignatureWrap signatureWrap)
		{
			string errMsg = "";
			string errMsgForChangeset = "";
			bool isChangesetError = false;
			Signature model = signatureWrap.Signature;
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
					Signature = model,
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

        public HttpResponseMessage Get(int id)
        {
            string errMsg = "";
            Model.Signature signature = BLL.SignatureManager.GetSignatureById(id,out errMsg);
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
                    signature = signature
                };
                json = JsonConvert.SerializeObject(msg2);
            }
            return new HttpResponseMessage(status)
            {
                Content = new StringContent(json, System.Text.Encoding.GetEncoding("UTF-8"), "application/json")
            };
        }

        public HttpResponseMessage Delete(int id)
        {
            string errMsg = "";
            try
            {
                string token = this.ControllerContext.Request.Headers.GetValues("Token").First();
                bool isChecked = TokenManage.CheckAuthorizer(token, PowerStatusCode.Scanner, out auth, out errMsg);
                if (isChecked)
                {
                    if (id > 0)
                    {
                        this.tryDelete(id.ToString(), out errMsg);
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
                var msg = new { };
                json = JsonConvert.SerializeObject(msg);
            }
            return new HttpResponseMessage(status)
            {
                Content = new StringContent(json, System.Text.Encoding.GetEncoding("UTF-8"), "application/json")
            };
        }

		private bool tryValidate(Signature model, out string errMsg)
        {
            errMsg = "";
            bool re = true;
            if (model.Name.Length > 10
                || model.Sign == null || model.Sign.Length < 20
                || model.Creater == 0)
            {
                errMsg = "输入数据不合法";
                re = false;
            }
            if (re && model.Creater != auth.UserId)
            {
                errMsg = "登录信息异常，请刷新浏览器以重启应用(APP请退出应用后重新打开)";
                re = false;
            }
            return re;
		}

		private bool tryPost(ref Signature model, out string errMsg)
		{
			errMsg = "";
			bool re = false;
			model.Creater = this.auth.UserId;
			model.CreatedDate = System.DateTime.Now;
			if (SignatureManager.AddSignature(ref model, out errMsg))
			{
				re = true;
			}
			return re;
		}

        private bool tryDelete(string ids, out string errMsg)
        {
            errMsg = "";
            bool re = false;
            if (ids != string.Empty)
            {
                if (SignatureManager.DeleteSignatureByIds(ids, out errMsg))
                {
                    re = true;
                }
            }
            return re;
        }
	}
}
