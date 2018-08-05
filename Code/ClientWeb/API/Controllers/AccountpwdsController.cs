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
    public class AccountpwdsController : ApiController
    {
        private Authorizer auth;

        public HttpResponseMessage Post(AccountpwdWrap accountpwdWrap)
        {
            string errMsg = "";
            string errMsgForChangeset = "";
            bool isChangesetError = false;
            Accountpwd accountpwd = accountpwdWrap.Accountpwd;
            Model.Changeset changeset = new Model.Changeset();
            try
            {
                string token = this.ControllerContext.Request.Headers.GetValues("Token").First();
                bool isChecked = false;
                if (TokenManage.CheckAuthorizer(token, PowerStatusCode.None,out auth, out errMsg))
                {
                    if (auth.UserId == accountpwd.User)
                    {
                        isChecked = true;
                    }
                    else {
                        errMsg = "密码不能被本人以外其他用户修改";
                    }
                }
                if (isChecked)
                {
                    if (tryValidate(accountpwd, out errMsg))
                    {
                        if (this.tryPost(ref accountpwd, out errMsg))
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
                var msg = new { Accountpwd = accountpwd, Changeset = changeset, IsChangesetError = isChangesetError, ErrMsgForChangeset = errMsgForChangeset };
                json = JsonConvert.SerializeObject(msg);
            }
            return new HttpResponseMessage(status)
            {
                Content = new StringContent(json, System.Text.Encoding.GetEncoding("UTF-8"), "application/json")
            };
        }

        private bool tryValidate(Model.Accountpwd model,out string errMsg)
        {
            errMsg = "";
            bool re = true;
            if (model.NewPassword == null
                || model.NewPassword.Length == 0
                || model.NewPassword != model.ConfirmPassword)
            {
                errMsg = "输入数据不合法";
                re = false;
            }
            return re;
        }

        private bool tryPost(ref Model.Accountpwd model, out string errMsg)
        {
            errMsg = "";
            model.OldPassword = Function.Encrypt(model.OldPassword.Trim(), 2);
            model.NewPassword = Function.Encrypt(model.NewPassword.Trim(), 2);
            if (UserManager.Updatepwd(ref model, out errMsg))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}