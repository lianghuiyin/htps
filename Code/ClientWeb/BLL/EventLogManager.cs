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
    /// 地点业务逻辑类
    /// </summary>
    public static class EventLogManager
    {
        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static bool AddEventLog<T>(Model.EventLog model, T obj)
        {
            return EventLogService.AddEventLog<T>(model, obj);
        }
    }
}
