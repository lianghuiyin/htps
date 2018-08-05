using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;
using System.Collections.Generic;
using BLL;
using Model;
using Newtonsoft.Json;
using System.Web;
using Utility;
using System.IO;

namespace API.Controllers
{
	public class DownloadsController : ApiController
    {
        public HttpResponseMessage Get(string name)
        {
            try
            {
                HttpResponseMessage hrm = new HttpResponseMessage();
                string path = HttpContext.Current.Server.MapPath("../download//" + name);
                FileInfo fi = new FileInfo(path);
                if (fi.Exists)
                {
                    hrm.StatusCode = HttpStatusCode.OK;
                    Stream stream = new FileStream(path, FileMode.Open);
                    hrm.Content = new StreamContent(stream);
                    hrm.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = HttpUtility.UrlEncode(fi.Name, System.Text.Encoding.UTF8)
                    };
                    hrm.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    hrm.Content.Headers.ContentLength = stream.Length;
                }
                else
                {
                    hrm.StatusCode = (HttpStatusCode)422;
                }

                return hrm;
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }
        }
    }
}
