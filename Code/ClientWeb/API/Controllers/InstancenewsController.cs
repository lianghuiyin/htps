﻿using System;
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
    public class InstancenewsController : ApiController
    {
        private Authorizer auth;

        public HttpResponseMessage Post(InstancenewWrap instancenewWrap)
        {
            string errMsg = "";
            string errMsgForChangeset = "";
            bool isChangesetError = false;
            Instancenew model = instancenewWrap.Instancenew;
            Model.Changeset changeset = new Model.Changeset();
            try
            {
                string token = this.ControllerContext.Request.Headers.GetValues("Token").First();
                bool isChecked = TokenManage.CheckAuthorizer(token, PowerStatusCode.Manager, out auth, out errMsg);
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
                var msg = new { Instancenew = model, Changeset = changeset, IsChangesetError = isChangesetError, ErrMsgForChangeset = errMsgForChangeset };
                json = JsonConvert.SerializeObject(msg);
            }
            return new HttpResponseMessage(status)
            {
                Content = new StringContent(json, System.Text.Encoding.GetEncoding("UTF-8"), "application/json")
            };
        }
 
        private bool tryValidate(Model.Instancenew model, out string errMsg)
        {
            errMsg = "";
            bool re = true;
            if (model.Car == 0 || model.Project == 0 || model.Department == 0
                || model.UserName == null || model.UserName.Length == 0 || model.UserName.Length > 20
                || model.Oils == null || model.Oils.Count == 0
                || model.Goal.Length > 200
                || model.StartDate == null || model.EndDate == null
                || model.StartInfo.Length > 200
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

        private bool tryPost(ref Model.Instancenew model, out string errMsg)
        {
            errMsg = "";
            bool re = false;
            model.Creater = this.auth.UserId;
            model.CreatedDate = DateTime.Now;
            if (InstanceManager.AddInstance(ref model, out errMsg))
            {
                re = true;
            }
            return re;
        }
    }
}