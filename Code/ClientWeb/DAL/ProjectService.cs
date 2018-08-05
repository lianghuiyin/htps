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
    /// 项目数据访问类
    /// </summary>
    public static class ProjectService
    {
        /// <summary>
        /// 添加项目
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddProject(ref Model.Project model, out string errMsg)
        {
            errMsg = "";
            try
            {
                model.Name = model.Name.Trim();
                model.Description = model.Description.Trim();
                SqlParameter[] para = new SqlParameter[] 
                {
                    new SqlParameter("@Name", model.Name),
                    new SqlParameter("@IsEnable", model.IsEnable),
                    new SqlParameter("@Description", model.Description),
                    new SqlParameter("@Creater", model.Creater),
                    new SqlParameter("@CreatedDate", model.CreatedDate),
                    new SqlParameter("@Modifier", model.Modifier),
                    new SqlParameter("@ModifiedDate", model.ModifiedDate),
                    new SqlParameter("@return",SqlDbType.Int)
                };
                para[7].Direction = ParameterDirection.ReturnValue;
                int i = DBHelper.ExecuteNonQuery(CommandType.StoredProcedure, "proc_ProjectInsert", para);
                if (i > 0)
                {
                    model.Id = int.Parse(para[7].Value.ToString());
                    EventLog e = new EventLog();
                    e.TargetIds = para[7].Value.ToString();
                    e.CodeTag = "AddProject";
                    e.LogName = "添加项目";
                    EventLogService.AddEventLog<Model.Project>(e, model);
                    return true;
                }
                else
                {
                    errMsg = "添加记录失败，受影响行数为0";
                    return false;
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                ErrorLog e = new ErrorLog();
                e.TargetIds = "0";
                e.CodeTag = "AddProject";
                e.LogName = "添加项目";
                e.ErrorMsg = ex.Message.ToString();
                ErrorLogService.AddErrorLog<Model.Project>(e, model);
                return false;
            }
        }

        /// <summary>
        /// 根据id修改项目
        /// </summary>
        /// <param name="Model.Project"></param>
        /// <returns></returns>
        public static bool ModifyProjectById(ref Model.Project model, out string errMsg)
        {
            errMsg = "";
            try
            {
                model.Name = model.Name.Trim();
                model.Description = model.Description.Trim();
                SqlParameter[] para = new SqlParameter[] 
                {
                    new SqlParameter("@Id", model.Id),
                    new SqlParameter("@Name", model.Name),
                    new SqlParameter("@IsEnable", model.IsEnable),
                    new SqlParameter("@Description", model.Description),
                    new SqlParameter("@Modifier", model.Modifier),
                    new SqlParameter("@ModifiedDate", model.ModifiedDate)
                };
                int i = DBHelper.ExecuteNonQuery(CommandType.StoredProcedure, "proc_ProjectByIdUpdate", para);
                if (i > 0)
                {
                    EventLog e = new EventLog();
                    e.TargetIds = model.Id.ToString();
                    e.CodeTag = "ModifyProjectById";
                    e.LogName = "修改项目";
                    EventLogService.AddEventLog<Model.Project>(e, model);
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
                e.CodeTag = "ModifyProjectById";
                e.LogName = "修改项目";
                e.ErrorMsg = ex.Message.ToString();
                ErrorLogService.AddErrorLog<Model.Project>(e, model);
                return false;
            }
        }

        /// <summary>
        /// 删除项目
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static bool DeleteProjectByIds(string ids, out string errMsg)
        {
            errMsg = "";
            try
            {
                SqlParameter[] para = new SqlParameter[] 
                {
                    new SqlParameter("@model", "Project"),
                    new SqlParameter("@ids", ids),
                    new SqlParameter("@return",SqlDbType.Bit)
                };
                para[2].Direction = ParameterDirection.ReturnValue;
                int i = DBHelper.ExecuteNonQuery(CommandType.StoredProcedure, "proc_ModelByIdsDelete", para);
                int stateTag = (int)para[2].Value;
                if (stateTag > 0)
                {
                    if (i > 0)
                    {
                        EventLog e = new EventLog();
                        e.TargetIds = ids.ToString();
                        e.CodeTag = "DeleteProjectByIds";
                        e.LogName = "删除项目";
                        EventLogService.AddEventLog<Model.Project>(e, null);
                        return true;
                    }
                    else
                    {
                        errMsg = "该记录已被删除！";
                        return false;
                    }
                }
                else if (stateTag == 0)
                {
                    errMsg = "选中记录中有关联数据不能删除！";
                    return false;
                }
                else if (stateTag == -1)
                {
                    errMsg = "网络繁忙，请稍后再试！";
                    return false;
                }
                else
                {
                    errMsg = "未知异常！";
                    return false;
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                ErrorLog e = new ErrorLog();
                e.TargetIds = ids.ToString();
                e.CodeTag = "DeleteProjectByIds";
                e.LogName = "删除项目";
                e.ErrorMsg = ex.Message.ToString();
                ErrorLogService.AddErrorLog<Model.Project>(e,null);
                return false;
            }
        }

        /// <summary>
        /// 检查是否重复
        /// </summary>
        /// <param name="Model.Project"></param>
        /// <returns></returns>
        public static bool CheckRepeat(Model.Project model)
        {
            try
            {
                string sql = "select * from Project where Name = @Name and Id <> @Id";
                SqlParameter[] para = new SqlParameter[]
                {
                    new SqlParameter("@Name", model.Name.Trim()),
                    new SqlParameter("@Id", model.Id)
                };
                DataTable temp = DBHelper.ExecuteGetDataTable(CommandType.Text, sql, para);
                if (temp.Rows.Count > 0)
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
                ErrorLog e = new ErrorLog();
                e.TargetIds = model.Id.ToString();
                e.CodeTag = "CheckRepeat";
                e.LogName = "检查是否重复-ForProject";
                e.ErrorMsg = ex.Message.ToString();
                ErrorLogService.AddErrorLog<Model.Project>(e, model);
                return true;
            }
        }
    }
}
