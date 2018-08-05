using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Model;
using DAL;
using Utility;

namespace BLL
{
    /// <summary>
    /// Changeset业务逻辑类
    /// </summary>
    public static class ChangesetManager
    {
        /// <summary>
        /// 获取Changeset
        /// </summary>
        /// <returns></returns>
        public static bool GetChangeset(DateTime syncToken, ref Model.Changeset model, out string errMsg)
        {
            return ChangesetService.GetChangeset(syncToken, ref model, out errMsg);
        }

        /// <summary>
        /// 尝试获取Changeset
        /// </summary>
        /// <returns></returns>
        public static bool tryFetch(DateTime syncToken, ref Model.Changeset model, out string errMsg)
        {
            model.Id = 1;
            TimeZone zone = TimeZone.CurrentTimeZone;
            TimeSpan offset = zone.GetUtcOffset(DateTime.Now);
            model.SyncToken = DateTime.UtcNow.AddHours(offset.TotalHours);
            return GetChangeset(syncToken, ref model, out errMsg);
        }
    }
}
