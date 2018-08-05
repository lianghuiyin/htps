using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Model;
using Utility;

namespace DAL
{
    /// <summary>
    /// 偏好设置数据访问类
    /// </summary>
    public static class PreferenceService
    {
        /// <summary>
        /// 根据id修改偏好设置
        /// </summary>
        /// <param name="Model.Preference"></param>
        /// <returns></returns>
        public static bool ModifyPreferenceById(ref Model.Preference model, out string errMsg)
        {
            errMsg = "";
            try
            {
                SqlParameter[] para = new SqlParameter[] 
                {
                    new SqlParameter("@Id", model.Id),
                    new SqlParameter("@ShortcutHour", model.ShortcutHour),
                    new SqlParameter("@FinishHour", model.FinishHour),
                    new SqlParameter("@Modifier", model.Modifier),
                    new SqlParameter("@ModifiedDate", model.ModifiedDate)
                };
                int i = DBHelper.ExecuteNonQuery(CommandType.StoredProcedure, "proc_PreferenceByIdUpdate", para);
                if (i > 0)
                {
                    EventLog e = new EventLog();
                    e.TargetIds = model.Id.ToString();
                    e.CodeTag = "ModifyPreferenceById";
                    e.LogName = "修改偏好设置";
                    EventLogService.AddEventLog<Model.Preference>(e, model);
                    return true;
                }
                else
                {
                    errMsg = "该记录已被删除，不能修改！";
                    return false;
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                ErrorLog e = new ErrorLog();
                e.TargetIds = model.Id.ToString();
                e.CodeTag = "ModifyPreferenceById";
                e.LogName = "修改偏好设置";
                e.ErrorMsg = ex.Message.ToString();
                ErrorLogService.AddErrorLog<Model.Preference>(e, model);
                return false;
            }
        }
    }
}
