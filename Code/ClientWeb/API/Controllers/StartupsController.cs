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
    public class StartupsController : ApiController
    {
        public HttpResponseMessage Get()
        {
            string errMsg = "";
            Model.Startup startup = new Model.Startup();
            try
            {
                string token = this.ControllerContext.Request.Headers.GetValues("Token").First();
                if (TokenManage.CheckAuthorizer(token,PowerStatusCode.None,out errMsg))
                {
                    StartupManager.tryFetch(ref startup, out errMsg);
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
                var msg = new { Startup = startup };
                json = JsonConvert.SerializeObject(msg);
            }
            return new HttpResponseMessage(status)
            {
                Content = new StringContent(json, System.Text.Encoding.GetEncoding("UTF-8"), "application/json")
            };
        }

        public HttpResponseMessage Post()
        {
            return Get();
        }

        public HttpResponseMessage Put()
        {
            return Get();
        }

        public string Options()
        {
            return null; // HTTP 200 response with empty body
        }
    }
}