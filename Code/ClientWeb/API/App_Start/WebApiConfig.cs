using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            //config.Routes.MapHttpRoute(
            //    name: "BillsFilterApi",
            //    routeTemplate: "api/{controller}/{project}/{department}/{startDate}/{endDate}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
            //config.Routes.MapHttpRoute(
            //    name: "BillsFilterApi",
            //    routeTemplate: "api/{controller}/{startDate}/{endDate}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
            //GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.Converters.Add(
            //    new Newtonsoft.Json.Converters.IsoDateTimeConverter()
            //    {
            //        DateTimeFormat = "yyyy-MM-dd hh:mm:ss"
            //    }
            //); 
        }
    }
}
