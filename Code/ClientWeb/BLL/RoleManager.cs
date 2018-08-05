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
    /// 角色逻辑类
    /// </summary>
    public static class RoleManager
    {
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddRole(ref Model.Role model, out string errMsg)
        {
            return RoleService.AddRole(ref model, out errMsg);
        }

        /// <summary>
        /// 根据id修改角色
        /// </summary>
        /// <param name="Model.Role"></param>
        /// <returns></returns>
        public static bool ModifyRoleById(ref Model.Role model, out string errMsg)
        {
            return RoleService.ModifyRoleById(ref model, out errMsg);
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static bool DeleteRoleByIds(string ids, out string errMsg)
        {
            return RoleService.DeleteRoleByIds(ids, out errMsg);
        }

        /// <summary>
        /// 检查是否重复
        /// </summary>
        /// <param name="Model.Role"></param>
        /// <returns></returns>
        public static bool CheckRepeat(Model.Role model)
        {
            return RoleService.CheckRepeat(model);
        }
    }
}
