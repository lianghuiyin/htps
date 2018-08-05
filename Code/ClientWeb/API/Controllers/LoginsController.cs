using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Text;
using Newtonsoft.Json;
using Utility;
using Model;
using BLL;

namespace API.Controllers
{
    public class LoginsController : ApiController
    {
        public HttpResponseMessage Post(LoginWrap loginWrap)
        {
            string errMsg = "";
            Login login = loginWrap.Login;
            try
            {
                if (login.LogName.Length > 20 || login.LogPassword.Length > 20)
                {
                    errMsg = "用户名或密码不正确";
                }
                else
                {
                    login.LogName = login.LogName.Trim();
                    login.LogPassword = Function.Encrypt(login.LogPassword.Trim(), 2);
                    UserManager.Login(ref login, out errMsg);
                }
                login.LogPassword = "";
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            if (errMsg.Length == 0)
            {
                //每次登录成功，需要排查download文件夹中所有创建时间超过8小时的文件并删除之
                string filePath = HttpContext.Current.Server.MapPath("..//download//");
                if (!Function.FilesDelete(filePath, "*.c*", 8 * 60 * 60))
                {
                    errMsg = "文件删除失败！";
                }
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
                var msg = new { Login = login };
                json = JsonConvert.SerializeObject(msg);

            }
            return new HttpResponseMessage(status)
            {
                Content = new StringContent(json, System.Text.Encoding.GetEncoding("UTF-8"), "application/json")
            };
        }
    }
}