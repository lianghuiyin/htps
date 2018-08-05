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
    public class ChangesetsController : ApiController
    {
        private Authorizer auth;
        public HttpResponseMessage Get()
        {
            string errMsg = "";
            Model.Changeset changeset = new Model.Changeset();
            try
            {
                string token = this.ControllerContext.Request.Headers.GetValues("Token").First();
                if (TokenManage.CheckAuthorizer(token, PowerStatusCode.None, out auth, out errMsg))
                {
                    ChangesetManager.tryFetch(auth.Sync, ref changeset, out errMsg);
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
                var msg = new { Changeset = changeset };
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
    }
}