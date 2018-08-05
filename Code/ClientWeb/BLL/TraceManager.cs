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
    /// 申请单履历逻辑类
    /// </summary>
    public static class TraceManager
    {
        /// <summary>
        /// 获取车辆申请单履历
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static IList<Model.Trace> GetTracesByInstance(int instance, out string errMsg)
        {
            return TraceService.GetTracesByInstance(instance, out errMsg);
        }
    }
}
