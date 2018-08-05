using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Text;
using Newtonsoft.Json;
using Utility;
using Model;
using BLL;

namespace API.Controllers
{
    public class ResetpwdsController : ApiController
    {
        private Authorizer auth;

        public HttpResponseMessage Post(ResetpwdWrap resetpwdWrap)
        {
            string errMsg = "";
            string errMsgForChangeset = "";
            bool isChangesetError = false;
            Resetpwd model = resetpwdWrap.Resetpwd;
            Model.Changeset changeset = new Model.Changeset();
            try
            {
                string token = this.ControllerContext.Request.Headers.GetValues("Token").First();
                bool isChecked = TokenManage.CheckAuthorizer(token, PowerStatusCode.Systemer, out auth, out errMsg);
                if (isChecked)
                {
                    if (tryValidate(model, out errMsg))
                    {
                        if (this.tryPost(ref model, out errMsg))
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
                model.Id = 1;
                status = HttpStatusCode.OK;
                var msg = new { Resetpwd = model, Changeset = changeset, IsChangesetError = isChangesetError, ErrMsgForChangeset = errMsgForChangeset };
                json = JsonConvert.SerializeObject(msg);
            }
            return new HttpResponseMessage(status)
            {
                Content = new StringContent(json, System.Text.Encoding.GetEncoding("UTF-8"), "application/json")
            };
        }
 
        private bool tryValidate(Model.Resetpwd model, out string errMsg)
        {
            errMsg = "";
            bool re = true;
            if (model.NewPassword == null || model.NewPassword.Length == 0 || model.NewPassword.Length > 20)
            {
                errMsg = "输入数据不合法";
                re = false;
            }
            return re;
        }

        private bool tryPost(ref Model.Resetpwd model, out string errMsg)
        {
            errMsg = "";
            bool re = false;
            model.NewPassword = Function.Encrypt(model.NewPassword.Trim(), 2);
            if (UserManager.Resetpwd(ref model, out errMsg))
            {
                re = true;
            }
            return re;
        }
    }
}