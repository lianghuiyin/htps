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
    public class UsersController : ApiController
    {
        private Authorizer auth;

        public HttpResponseMessage Post(UserWrap userWrap)
        {
            string errMsg = "";
            string errMsgForChangeset = "";
            bool isChangesetError = false;
            User model = userWrap.User;
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
                status = HttpStatusCode.OK;
                var msg = new { User = model, Changeset = changeset, IsChangesetError = isChangesetError, ErrMsgForChangeset = errMsgForChangeset };
                json = JsonConvert.SerializeObject(msg);
            }
            return new HttpResponseMessage(status)
            {
                Content = new StringContent(json, System.Text.Encoding.GetEncoding("UTF-8"), "application/json")
            };
        }

        public HttpResponseMessage Put(int id,UserWrap userWrap)
        {
            string errMsg = "";
            string errMsgForChangeset = "";
            bool isChangesetError = false;
            User model = userWrap.User;
            model.Id = id;
            Model.Changeset changeset = new Model.Changeset();
            try
            {
                string token = this.ControllerContext.Request.Headers.GetValues("Token").First();
                auth = TokenManage.GetAuthorizer(token,out errMsg);
                bool isChecked = false;
                if (auth.IsAuthorized && auth.UserId == model.Id)
                {
                    //自己修改自己的账户信息
                    isChecked = true;
                }
                else if (auth.IsAuthorized && TokenManage.CheckAuthorizer(auth, PowerStatusCode.Systemer, out errMsg))
                {
                    isChecked = true;
                }
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
            if (errMsg.Length > 0)
            {
                status = (HttpStatusCode)422;
                var msg = new { errors = new { ServerSideError = errMsg } };
                json = JsonConvert.SerializeObject(msg);
            }
            else
            {
                status = HttpStatusCode.OK;
                var msg = new { User = model, Changeset = changeset, IsChangesetError = isChangesetError, ErrMsgForChangeset = errMsgForChangeset };
                json = JsonConvert.SerializeObject(msg);
            }
            return new HttpResponseMessage(status)
            {
                Content = new StringContent(json, System.Text.Encoding.GetEncoding("UTF-8"), "application/json")
            };
        }

        public HttpResponseMessage Delete(int id)
        {
            string errMsg = "";
            string errMsgForChangeset = "";
            bool isChangesetError = false;
            Model.Changeset changeset = new Model.Changeset();
            try
            {
                string token = this.ControllerContext.Request.Headers.GetValues("Token").First();
                bool isChecked = TokenManage.CheckAuthorizer(token, PowerStatusCode.Systemer, out auth, out errMsg);
                if (isChecked)
                {
                    if (id > 0)
                    {
                        if (this.tryDelete(id.ToString(), out errMsg))
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
                var msg = new { Changeset = changeset, IsChangesetError = isChangesetError, ErrMsgForChangeset = errMsgForChangeset };
                json = JsonConvert.SerializeObject(msg);
            }
            return new HttpResponseMessage(status)
            {
                Content = new StringContent(json, System.Text.Encoding.GetEncoding("UTF-8"), "application/json")
            };
        }

        private bool tryValidate(Model.User model,out string errMsg)
        {
            errMsg = "";
            bool re = true;
            if ((model.Phone.Length == 0 && model.Email.Length == 0)
                || model.Phone.Length > 20
                || model.Email.Length > 100
                || model.Name == null || model.Name.Length == 0 || model.Name.Length > 20
                || model.Signature.Length > 200
                || model.Role <= 0)
            {
                errMsg = "输入数据不合法";
                re = false;
            }
            if (re && UserManager.CheckRepeatForPhone(model))
            {
                errMsg = "手机号不能重复";
                re = false;
            }
            if (re && UserManager.CheckRepeatForEmail(model))
            {
                errMsg = "邮箱不能重复";
                re = false;
            }
            if (re && model.Modifier != auth.UserId)
            {
                errMsg = "登录信息异常，请刷新浏览器以重启应用(APP请退出应用后重新打开)";
                re = false;
            }
            return re;
        }

        private bool tryPut(ref Model.User model, out string errMsg)
        {
            errMsg = "";
            bool re = false;
            model.Modifier = this.auth.UserId;
            model.ModifiedDate = DateTime.Now;
            if (UserManager.ModifyUserById(ref model, out errMsg))
            {
                re = true;
            }
            return re;
        }

        private bool tryPost(ref Model.User model, out string errMsg)
        {
            errMsg = "";
            bool re = false;
            model.Creater = this.auth.UserId;
            model.CreatedDate = DateTime.Now;
            model.Modifier = this.auth.UserId;
            model.ModifiedDate = DateTime.Now;
            if (UserManager.AddUser(ref model, out errMsg))
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
                if (UserManager.DeleteUserByIds(ids, out errMsg))
                {
                    re = true;
                }
            }
            return re;
        }
    }
}