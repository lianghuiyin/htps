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
    /// 部门逻辑类
    /// </summary>
    public static class DepartmentManager
    {
        /// <summary>
        /// 添加部门
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddDepartment(ref Model.Department model, out string errMsg)
        {
            return DepartmentService.AddDepartment(ref model, out errMsg);
        }

        /// <summary>
        /// 根据id修改部门
        /// </summary>
        /// <param name="Model.Department"></param>
        /// <returns></returns>
        public static bool ModifyDepartmentById(ref Model.Department model, out string errMsg)
        {
            return DepartmentService.ModifyDepartmentById(ref model, out errMsg);
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static bool DeleteDepartmentByIds(string ids, out string errMsg)
        {
            return DepartmentService.DeleteDepartmentByIds(ids, out errMsg);
        }

        /// <summary>
        /// 检查是否重复
        /// </summary>
        /// <param name="Model.Department"></param>
        /// <returns></returns>
        public static bool CheckRepeat(Model.Department model)
        {
            return DepartmentService.CheckRepeat(model);
        }
    }
}
