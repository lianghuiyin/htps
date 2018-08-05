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
    public class PreferencesController : ApiController
    {
        private Authorizer auth;

        public HttpResponseMessage Put(int id, PreferenceWrap preferenceWrap)
        {
            string errMsg = "";
            string errMsgForChangeset = "";
            bool isChangesetError = false;
            Preference model = preferenceWrap.Preference;
            model.Id = id;
            Model.Changeset changeset = new Model.Changeset();
            try
            {
                string token = this.ControllerContext.Request.Headers.GetValues("Token").First();
                bool isChecked = TokenManage.CheckAuthorizer(token, PowerStatusCode.Systemer, out auth, out errMsg);
                if (isChecked)
                {
                    if (tryValidate(model, out errMsg))
                    {
                        if (this.tryPut(ref model, out errMsg))
                        {
                            if (!ChangesetManager.tryFetch(auth.Sync, ref changeset, out errMsgForChangeset)) {
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
            //errMsg = "网络。。。 ";
            if (errMsg.Length > 0)
            {
                status = (HttpStatusCode)422;
                var msg = new { errors = new { ServerSideError = errMsg } };
                json = JsonConvert.SerializeObject(msg);
            }
            else
            {
                status = HttpStatusCode.OK;
                var msg = new { Preference = model, Changeset = changeset, IsChangesetError = isChangesetError, ErrMsgForChangeset = errMsgForChangeset };
                json = JsonConvert.SerializeObject(msg);
            }
            return new HttpResponseMessage(status)
            {
                Content = new StringContent(json, System.Text.Encoding.GetEncoding("UTF-8"), "application/json")
            };
        }

        private bool tryValidate(Model.Preference model, out string errMsg)
        {
            errMsg = "";
            bool re = true;
            if (model.ShortcutHour > 23 || model.ShortcutHour < 0)
            {
                errMsg = "输入数据不合法";
                re = false;
            }
            if (model.FinishHour <= 0)
            {
                errMsg = "输入数据不合法";
                re = false;
            }
            if (re && model.Modifier != auth.UserId)
            {
                errMsg = "登录信息异常，请刷新浏览器以重启应用(APP请退出应用后重新打开)";
                re = false;
            }
            return re;
        }

        private bool tryPut(ref Model.Preference model, out string errMsg)
        {
            errMsg = "";
            bool re = false;
            model.Modifier = this.auth.UserId;
            model.ModifiedDate = DateTime.Now;
            if (PreferenceManager.ModifyPreferenceById(ref model, out errMsg))
            {
                re = true;
            }
            return re;
        }
    }
}