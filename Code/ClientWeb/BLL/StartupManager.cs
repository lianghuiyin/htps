using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Model;
using DAL;

namespace BLL
{
    /// <summary>
    /// Startup业务逻辑类
    /// </summary>
    public static class StartupManager
    {
        /// <summary>
        /// 获取Startup
        /// </summary>
        /// <returns></returns>
        public static bool GetStartup(ref Model.Startup model, out string errMsg)
        {
            return StartupService.GetStartup(ref model, out errMsg);
        }

        /// <summary>
        /// 尝试获取Startup
        /// </summary>
        /// <returns></returns>
        public static bool tryFetch(ref Model.Startup model, out string errMsg)
        {
            model.Id = 1;
            TimeZone zone = TimeZone.CurrentTimeZone;
            TimeSpan offset = zone.GetUtcOffset(DateTime.Now);
            model.SyncToken = DateTime.UtcNow.AddHours(offset.TotalHours);
            return GetStartup(ref model, out errMsg);
        }
    }
}
