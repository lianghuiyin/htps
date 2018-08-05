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
    /// 油品逻辑类
    /// </summary>
    public static class OilManager
    {
        /// <summary>
        /// 添加油品
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddOil(ref Model.Oil model, out string errMsg)
        {
            return OilService.AddOil(ref model, out errMsg);
        }

        /// <summary>
        /// 根据id修改油品
        /// </summary>
        /// <param name="Model.Oil"></param>
        /// <returns></returns>
        public static bool ModifyOilById(ref Model.Oil model, out string errMsg)
        {
            return OilService.ModifyOilById(ref model, out errMsg);
        }

        /// <summary>
        /// 删除油品
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static bool DeleteOilByIds(string ids, out string errMsg)
        {
            return OilService.DeleteOilByIds(ids, out errMsg);
        }

        /// <summary>
        /// 检查是否重复
        /// </summary>
        /// <param name="Model.Oil"></param>
        /// <returns></returns>
        public static bool CheckRepeat(Model.Oil model)
        {
            return OilService.CheckRepeat(model);
        }
    }
}
