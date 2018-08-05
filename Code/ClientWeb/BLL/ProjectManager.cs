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
    /// 项目逻辑类
    /// </summary>
    public static class ProjectManager
    {
        /// <summary>
        /// 添加项目
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddProject(ref Model.Project model, out string errMsg)
        {
            return ProjectService.AddProject(ref model, out errMsg);
        }

        /// <summary>
        /// 根据id修改项目
        /// </summary>
        /// <param name="Model.Project"></param>
        /// <returns></returns>
        public static bool ModifyProjectById(ref Model.Project model, out string errMsg)
        {
            return ProjectService.ModifyProjectById(ref model, out errMsg);
        }

        /// <summary>
        /// 删除项目
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static bool DeleteProjectByIds(string ids, out string errMsg)
        {
            return ProjectService.DeleteProjectByIds(ids, out errMsg);
        }

        /// <summary>
        /// 检查是否重复
        /// </summary>
        /// <param name="Model.Project"></param>
        /// <returns></returns>
        public static bool CheckRepeat(Model.Project model)
        {
            return ProjectService.CheckRepeat(model);
        }
    }
}
