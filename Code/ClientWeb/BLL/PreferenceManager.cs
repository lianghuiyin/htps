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
    /// 偏好设置逻辑类
    /// </summary>
    public static class PreferenceManager
    {
        /// <summary>
        /// 根据id修改偏好设置
        /// </summary>
        /// <param name="Model.Preference"></param>
        /// <returns></returns>
        public static bool ModifyPreferenceById(ref Model.Preference model, out string errMsg)
        {
            return PreferenceService.ModifyPreferenceById(ref model, out errMsg);
        }
    }
}
