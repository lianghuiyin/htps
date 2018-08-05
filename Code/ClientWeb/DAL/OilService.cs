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
    /// 油品数据访问类
    /// </summary>
    public static class OilService
    {
        /// <summary>
        /// 添加油品
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddOil(ref Model.Oil model, out string errMsg)
        {
            errMsg = "";
            try
            {
                model.Name = model.Name.Trim();
                model.Description = model.Description.Trim();
                SqlParameter[] para = new SqlParameter[] 
                {
                    new SqlParameter("@Name", model.Name),
                    new SqlParameter("@YellowRate", model.YellowRate),
                    new SqlParameter("@RedRate", model.RedRate),
                    new SqlParameter("@Description", model.Description),
                    new SqlParameter("@Creater", model.Creater),
                    new SqlParameter("@CreatedDate", model.CreatedDate),
                    new SqlParameter("@Modifier", model.Modifier),
                    new SqlParameter("@ModifiedDate", model.ModifiedDate),
                    new SqlParameter("@return",SqlDbType.Int)
                };
                para[8].Direction = ParameterDirection.ReturnValue;
                int i = DBHelper.ExecuteNonQuery(CommandType.StoredProcedure, "proc_OilInsert", para);
                if (i > 0)
                {
                    model.Id = int.Parse(para[8].Value.ToString());
                    EventLog e = new EventLog();
                    e.TargetIds = para[8].Value.ToString();
                    e.CodeTag = "AddOil";
                    e.LogName = "添加油品";
                    EventLogService.AddEventLog<Model.Oil>(e, model);
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
                e.CodeTag = "AddOil";
                e.LogName = "添加油品";
                e.ErrorMsg = ex.Message.ToString();
                ErrorLogService.AddErrorLog<Model.Oil>(e, model);
                return false;
            }
        }

        /// <summary>
        /// 根据id修改油品
        /// </summary>
        /// <param name="Model.Oil"></param>
        /// <returns></returns>
        public static bool ModifyOilById(ref Model.Oil model, out string errMsg)
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
                    new SqlParameter("@YellowRate", model.YellowRate),
                    new SqlParameter("@RedRate", model.RedRate),
                    new SqlParameter("@Description", model.Description),
                    new SqlParameter("@Modifier", model.Modifier),
                    new SqlParameter("@ModifiedDate", model.ModifiedDate)
                };
                int i = DBHelper.ExecuteNonQuery(CommandType.StoredProcedure, "proc_OilByIdUpdate", para);
                if (i > 0)
                {
                    EventLog e = new EventLog();
                    e.TargetIds = model.Id.ToString();
                    e.CodeTag = "ModifyOilById";
                    e.LogName = "修改油品";
                    EventLogService.AddEventLog<Model.Oil>(e, model);
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
                e.CodeTag = "ModifyOilById";
                e.LogName = "修改油品";
                e.ErrorMsg = ex.Message.ToString();
                ErrorLogService.AddErrorLog<Model.Oil>(e, model);
                return false;
            }
        }

        /// <summary>
        /// 删除油品
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static bool DeleteOilByIds(string ids, out string errMsg)
        {
            errMsg = "";
            try
            {
                SqlParameter[] para = new SqlParameter[] 
                {
                    new SqlParameter("@model", "Oil"),
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
                        e.CodeTag = "DeleteOilByIds";
                        e.LogName = "删除油品";
                        EventLogService.AddEventLog<Model.Oil>(e, null);
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
                e.CodeTag = "DeleteOilByIds";
                e.LogName = "删除油品";
                e.ErrorMsg = ex.Message.ToString();
                ErrorLogService.AddErrorLog<Model.Oil>(e,null);
                return false;
            }
        }

        /// <summary>
        /// 检查是否重复
        /// </summary>
        /// <param name="Model.Oil"></param>
        /// <returns></returns>
        public static bool CheckRepeat(Model.Oil model)
        {
            try
            {
                string sql = "select * from Oil where Name = @Name and Id <> @Id";
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
                e.LogName = "检查是否重复-ForOil";
                e.ErrorMsg = ex.Message.ToString();
                ErrorLogService.AddErrorLog<Model.Oil>(e, model);
                return true;
            }
        }
    }
}
