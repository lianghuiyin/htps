using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Model;
using Utility;
using System.Reflection;

namespace DAL
{
    /// <summary>
    /// 事务日志访问类
    /// </summary>
    public static class EventLogService
    {
        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static bool AddEventLog<T>(Model.EventLog model, T obj)
        {
            try
            {
                if (obj != null)
                {
                    model.LogContent = JSONHelper.Serialize<T>(obj);
                }
                else
                {
                    if (model.LogContent == null)
                    {
                        model.LogContent = "";
                    }
                }

                model.User = Sessions.GetIntSession("Id");
                model.UserName = Sessions.GetStringSession("Name");
                model.TargetIds = Function.KeepStringLength(model.TargetIds, 100);
                model.CodeTag = Function.KeepStringLength(model.CodeTag, 50);
                model.LogName = Function.KeepStringLength(model.LogName, 50);
                model.LogContent = Function.KeepStringLength(model.LogContent, 500);
                model.IpAddr = Function.KeepStringLength(Function.GetIpAddr(), 50);
                model.UrlPath = Function.KeepStringLength(Function.GetUrlPath(), 50);
                model.IsSpecial = model.IsSpecial;

                SqlParameter[] para = new SqlParameter[] 
                {
                    new SqlParameter("@User", model.User),
                    new SqlParameter("@UserName", model.UserName),
                    new SqlParameter("@TargetIds", model.TargetIds),
                    new SqlParameter("@CodeTag", model.CodeTag),
                    new SqlParameter("@LogName", model.LogName),
                    new SqlParameter("@LogContent", model.LogContent),
                    new SqlParameter("@IpAddr", model.IpAddr),
                    new SqlParameter("@UrlPath", model.UrlPath),
                    new SqlParameter("@IsSpecial", model.IsSpecial)
                };
                int i = DBHelper.ExecuteNonQuery(CommandType.Text, "insert into EventLog([User],UserName,TargetIds,CodeTag,LogName,LogContent,IpAddr,UrlPath,IsSpecial) values (@User,@UserName,@TargetIds,@CodeTag,@LogName,@LogContent,@IpAddr,@UrlPath,@IsSpecial)", para);
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
