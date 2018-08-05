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
    /// 申请单数据访问类
    /// </summary>
    public static class InstanceService
    {
        /// <summary>
        /// 新建申请单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddInstance(ref Model.Instancenew model, out string errMsg)
        {
            errMsg = "";
            try
            {
                model.UserName = model.UserName.Trim();
                int[] oils = model.Oils.ToArray();
                string[] arrayOils = Array.ConvertAll(oils, delegate(int s) { return s.ToString(); });
                SqlParameter[] para = new SqlParameter[] 
                {
                    new SqlParameter("@Car", model.Car),
                    new SqlParameter("@Project", model.Project),
                    new SqlParameter("@Department", model.Department),
                    new SqlParameter("@UserName", model.UserName),
                    new SqlParameter("@Oils", string.Join(",",arrayOils)),
                    new SqlParameter("@Goal", model.Goal.Trim()),
                    new SqlParameter("@StartDate", model.StartDate),
                    new SqlParameter("@EndDate", model.EndDate),
                    new SqlParameter("@StartInfo", model.StartInfo.Trim()),
                    new SqlParameter("@Creater", model.Creater),
                    new SqlParameter("@CreatedDate", model.CreatedDate),
                    new SqlParameter("@OutState",SqlDbType.Int),
                    new SqlParameter("@return",SqlDbType.Int)
                };
                para[11].Direction = ParameterDirection.Output;
                para[12].Direction = ParameterDirection.ReturnValue;
                DBHelper.ExecuteNonQuery(CommandType.StoredProcedure, "proc_InstanceInsert", para);
                int outState = int.Parse(para[11].Value.ToString());
                int returnValue = int.Parse(para[12].Value.ToString());
                if (returnValue > 0)
                {
                    EventLog e = new EventLog();
                    e.TargetIds = returnValue.ToString();
                    e.CodeTag = "AddInstance";
                    e.LogName = "新建申请单";
                    EventLogService.AddEventLog<Model.Instancenew>(e, model);
                    return true;
                }
                else
                {
                    switch (outState)
                    {
                        case -100:
                            errMsg = "起止时间设置有误";
                            break;
                        case -1:
                            errMsg = "添加申请单失败";
                            break;
                        case -2:
                            errMsg = "添加申请单履历失败";
                            break;
                        case -3:
                            errMsg = "更新车辆信息失败";
                            break;
                        default:
                            errMsg = "异常错误";
                            break;
                    }
                    ErrorLog e = new ErrorLog();
                    e.TargetIds = model.Car.ToString();
                    e.CodeTag = "AddInstance";
                    e.LogName = "新建申请单";
                    e.ErrorMsg = errMsg;
                    ErrorLogService.AddErrorLog<Model.Instancenew>(e, model);
                    return false;
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                ErrorLog e = new ErrorLog();
                e.TargetIds = "0";
                e.CodeTag = "AddInstance";
                e.LogName = "新建申请单";
                e.ErrorMsg = ex.Message.ToString();
                ErrorLogService.AddErrorLog<Model.Instancenew>(e, model);
                return false;
            }
        }

        /// <summary>
        /// 新建申请单履历
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddTrace(ref Model.Tracenew model, out string errMsg)
        {
            errMsg = "";
            try
            {
                model.Status = model.Status.Trim();
                model.UserName = model.UserName.Trim();
                model.Goal = model.Goal.Trim();
                model.StartInfo = model.StartInfo.Trim();
                int[] oils = model.Oils.ToArray();
                string[] arrayOils = Array.ConvertAll(oils, delegate(int s) { return s.ToString(); });
                SqlParameter[] para = new SqlParameter[] 
                {
                    new SqlParameter("@Car", model.Car),
                    new SqlParameter("@Instance", model.Instance),
                    new SqlParameter("@PreviousTrace", model.PreviousTrace),
                    new SqlParameter("@Status", model.Status),
                    new SqlParameter("@Project", model.Project),
                    new SqlParameter("@Department", model.Department),
                    new SqlParameter("@UserName", model.UserName),
                    new SqlParameter("@Oils", string.Join(",",arrayOils)),
                    new SqlParameter("@Goal", model.Goal),
                    new SqlParameter("@StartDate", model.StartDate),
                    new SqlParameter("@EndDate", model.EndDate),
                    new SqlParameter("@StartInfo", model.StartInfo),
                    new SqlParameter("@Creater", model.Creater),
                    new SqlParameter("@CreatedDate", model.CreatedDate),
                    new SqlParameter("@OutState",SqlDbType.Int),
                    new SqlParameter("@return",SqlDbType.Int)
                };
                para[14].Direction = ParameterDirection.Output;
                para[15].Direction = ParameterDirection.ReturnValue;
                DBHelper.ExecuteNonQuery(CommandType.StoredProcedure, "proc_TraceInsert", para);
                int outState = int.Parse(para[14].Value.ToString());
                int returnValue = int.Parse(para[15].Value.ToString());
                if (returnValue > 0)
                {
                    EventLog e = new EventLog();
                    e.TargetIds = returnValue.ToString();
                    e.CodeTag = "AddTrace";
                    e.LogName = "新建申请单履历";
                    EventLogService.AddEventLog<Model.Tracenew>(e, model);
                    return true;
                }
                else
                {
                    switch (outState)
                    {
                        case -100:
                            errMsg = "该申请单已归档，不能修改";
                            break;
                        case -200:
                            errMsg = "该申请单处于待审核状态，不能修改申请单，如果需要修改，请先取回该申请单";
                            break;
                        case -300:
                            errMsg = "起止时间设置有误";
                            break;
                        case -1:
                            errMsg = "添加申请单履历失败";
                            break;
                        case -2:
                            errMsg = "更新(提交)申请单信息失败";
                            break;
                        case -3:
                            errMsg = "更新申请单履历结束状态失败";
                            break;
                        case -4:
                            errMsg = "更新申请单信息失败";
                            break;
                        case -5:
                            errMsg = "更新车辆信息失败";
                            break;
                        default:
                            errMsg = "异常错误";
                            break;
                    }
                    ErrorLog e = new ErrorLog();
                    e.TargetIds = model.Car.ToString();
                    e.CodeTag = "AddTrace";
                    e.LogName = "新建申请单履历";
                    e.ErrorMsg = errMsg;
                    ErrorLogService.AddErrorLog<Model.Tracenew>(e, model);
                    return false;
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                ErrorLog e = new ErrorLog();
                e.TargetIds = "0";
                e.CodeTag = "AddTrace";
                e.LogName = "新建申请单履历";
                e.ErrorMsg = ex.Message.ToString();
                ErrorLogService.AddErrorLog<Model.Tracenew>(e, model);
                return false;
            }
        }

        /// <summary>
        /// 取回申请单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool RecaptureTrace(ref Model.Tracerecapture model, out string errMsg)
        {
            errMsg = "";
            try
            {
                model.EndInfo = model.EndInfo.Trim();
                SqlParameter[] para = new SqlParameter[] 
                {
                    new SqlParameter("@Trace", model.Trace),
                    new SqlParameter("@Instance", model.Instance),
                    new SqlParameter("@Car", model.Car),
                    new SqlParameter("@EndInfo", model.EndInfo),
                    new SqlParameter("@Creater", model.Creater),
                    new SqlParameter("@CreatedDate", model.CreatedDate),
                    new SqlParameter("@OutState",SqlDbType.Int),
                    new SqlParameter("@return",SqlDbType.Int)
                };
                para[6].Direction = ParameterDirection.Output;
                para[7].Direction = ParameterDirection.ReturnValue;
                DBHelper.ExecuteNonQuery(CommandType.StoredProcedure, "proc_TraceRecapture", para);
                int outState = int.Parse(para[6].Value.ToString());
                int returnValue = int.Parse(para[7].Value.ToString());
                if (returnValue > 0)
                {
                    EventLog e = new EventLog();
                    e.TargetIds = returnValue.ToString();
                    e.CodeTag = "RecaptureTrace";
                    e.LogName = "取回申请单";
                    EventLogService.AddEventLog<Model.Tracerecapture>(e, model);
                    return true;
                }
                else
                {
                    switch (outState)
                    {
                        case -100:
                            errMsg = "该申请单处于非待审核状态，可能已被其他用户取回，不能取回申请单";
                            break;
                        case -200:
                            errMsg = "该申请单待审核履历已结束，可能已被其他用户取回，不能取回申请单";
                            break;
                        case -1:
                            errMsg = "更新申请单履历信息失败";
                            break;
                        case -2:
                            errMsg = "更新申请单信息失败";
                            break;
                        case -3:
                            errMsg = "更新车辆信息失败";
                            break;
                        default:
                            errMsg = "异常错误";
                            break;
                    }
                    ErrorLog e = new ErrorLog();
                    e.TargetIds = model.Car.ToString();
                    e.CodeTag = "RecaptureTrace";
                    e.LogName = "取回申请单";
                    e.ErrorMsg = errMsg;
                    ErrorLogService.AddErrorLog<Model.Tracerecapture>(e, model);
                    return false;
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                ErrorLog e = new ErrorLog();
                e.TargetIds = "0";
                e.CodeTag = "RecaptureTrace";
                e.LogName = "取回申请单";
                e.ErrorMsg = ex.Message.ToString();
                ErrorLogService.AddErrorLog<Model.Tracerecapture>(e, model);
                return false;
            }
        }

        /// <summary>
        /// 归档申请单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool ArchiveInstance(ref Model.Instancearchive model, out string errMsg)
        {
            errMsg = "";
            try
            {
                SqlParameter[] para = new SqlParameter[] 
                {
                    new SqlParameter("@Instance", model.Instance),
                    new SqlParameter("@Car", model.Car),
                    new SqlParameter("@Creater", model.Creater),
                    new SqlParameter("@CreatedDate", model.CreatedDate),
                    new SqlParameter("@OutState",SqlDbType.Int),
                    new SqlParameter("@return",SqlDbType.Int)
                };
                para[4].Direction = ParameterDirection.Output;
                para[5].Direction = ParameterDirection.ReturnValue;
                DBHelper.ExecuteNonQuery(CommandType.StoredProcedure, "proc_InstanceArchive", para);
                int outState = int.Parse(para[4].Value.ToString());
                int returnValue = int.Parse(para[5].Value.ToString());
                if (returnValue > 0)
                {
                    EventLog e = new EventLog();
                    e.TargetIds = returnValue.ToString();
                    e.CodeTag = "ArchiveInstance";
                    e.LogName = "归档申请单";
                    EventLogService.AddEventLog<Model.Instancearchive>(e, model);
                    return true;
                }
                else
                {
                    switch (outState)
                    {
                        case -100:
                            errMsg = "该申请单处于已归档状态，可能已被其他用户归档，不能重复归档";
                            break;
                        case -1:
                            errMsg = "更新申请单履历归档状态失败";
                            break;
                        case -2:
                            errMsg = "更新申请单归档状态失败";
                            break;
                        case -3:
                            errMsg = "更新车辆信息失败";
                            break;
                        default:
                            errMsg = "异常错误";
                            break;
                    }
                    ErrorLog e = new ErrorLog();
                    e.TargetIds = model.Car.ToString();
                    e.CodeTag = "ArchiveInstance";
                    e.LogName = "归档申请单";
                    e.ErrorMsg = errMsg;
                    ErrorLogService.AddErrorLog<Model.Instancearchive>(e, model);
                    return false;
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                ErrorLog e = new ErrorLog();
                e.TargetIds = "0";
                e.CodeTag = "ArchiveInstance";
                e.LogName = "归档申请单";
                e.ErrorMsg = ex.Message.ToString();
                ErrorLogService.AddErrorLog<Model.Instancearchive>(e, model);
                return false;
            }
        }

        /// <summary>
        /// 审核申请单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool CheckInstance(ref Model.Instancecheck model, out string errMsg)
        {
            errMsg = "";
            try
            {
                model.Status = model.Status.Trim();
                model.EndInfo = model.EndInfo.Trim();
                SqlParameter[] para = new SqlParameter[] 
                {
                    new SqlParameter("@Trace", model.Trace),
                    new SqlParameter("@Instance", model.Instance),
                    new SqlParameter("@Car", model.Car),
                    new SqlParameter("@Status", model.Status),
                    new SqlParameter("@EndInfo", model.EndInfo),
                    new SqlParameter("@Creater", model.Creater),
                    new SqlParameter("@CreatedDate", model.CreatedDate),
                    new SqlParameter("@OutState",SqlDbType.Int),
                    new SqlParameter("@return",SqlDbType.Int)
                };
                para[7].Direction = ParameterDirection.Output;
                para[8].Direction = ParameterDirection.ReturnValue;
                DBHelper.ExecuteNonQuery(CommandType.StoredProcedure, "proc_InstanceCheck", para);
                int outState = int.Parse(para[7].Value.ToString());
                int returnValue = int.Parse(para[8].Value.ToString());
                if (returnValue > 0)
                {
                    EventLog e = new EventLog();
                    e.TargetIds = returnValue.ToString();
                    e.CodeTag = "CheckInstance";
                    e.LogName = "审核申请单";
                    EventLogService.AddEventLog<Model.Instancecheck>(e, model);
                    return true;
                }
                else
                {
                    switch (outState)
                    {
                        case -100:
                            errMsg = "该申请单已被归档，不能审核";
                            break;
                        case -200:
                            errMsg = "该申请单处于非待审核状态，可能已被其他用户审核或已被申请人取回，不能重复审核";
                            break;
                        case -300:
                            errMsg = "该申请单已被禁用，不能审核";
                            break;
                        case -400:
                            errMsg = "该申请单已被其他人审核过了，不能重复审核";
                            break;
                        case -1:
                            errMsg = "更新申请单履历失败";
                            break;
                        case -2:
                            errMsg = "更新申请单信息失败";
                            break;
                        case -3:
                            errMsg = "更新申请单状态失败";
                            break;
                        case -4:
                            errMsg = "更新车辆信息失败";
                            break;
                        default:
                            errMsg = "异常错误";
                            break;
                    }
                    ErrorLog e = new ErrorLog();
                    e.TargetIds = model.Car.ToString();
                    e.CodeTag = "CheckInstance";
                    e.LogName = "审核申请单";
                    e.ErrorMsg = errMsg;
                    ErrorLogService.AddErrorLog<Model.Instancecheck>(e, model);
                    return false;
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                ErrorLog e = new ErrorLog();
                e.TargetIds = "0";
                e.CodeTag = "CheckInstance";
                e.LogName = "审核申请单";
                e.ErrorMsg = ex.Message.ToString();
                ErrorLogService.AddErrorLog<Model.Instancecheck>(e, model);
                return false;
            }
        }

        /// <summary>
        /// 禁用申请单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool ForbidInstance(ref Model.Instanceforbid model, out string errMsg)
        {
            errMsg = "";
            try
            {
                model.Status = model.Status.Trim();
                model.StartInfo = model.StartInfo.Trim();
                SqlParameter[] para = new SqlParameter[] 
                {
                    new SqlParameter("@Trace", model.Trace),
                    new SqlParameter("@Instance", model.Instance),
                    new SqlParameter("@Car", model.Car),
                    new SqlParameter("@Status", model.Status),
                    new SqlParameter("@StartInfo", model.StartInfo),
                    new SqlParameter("@Creater", model.Creater),
                    new SqlParameter("@CreatedDate", model.CreatedDate),
                    new SqlParameter("@OutState",SqlDbType.Int),
                    new SqlParameter("@return",SqlDbType.Int)
                };
                para[7].Direction = ParameterDirection.Output;
                para[8].Direction = ParameterDirection.ReturnValue;
                DBHelper.ExecuteNonQuery(CommandType.StoredProcedure, "proc_InstanceForbid", para);
                int outState = int.Parse(para[7].Value.ToString());
                int returnValue = int.Parse(para[8].Value.ToString());
                if (returnValue > 0)
                {
                    EventLog e = new EventLog();
                    e.TargetIds = returnValue.ToString();
                    e.CodeTag = "ForbidInstance";
                    e.LogName = "禁用申请单";
                    EventLogService.AddEventLog<Model.Instanceforbid>(e, model);
                    return true;
                }
                else
                {
                    switch (outState)
                    {
                        case -100:
                            errMsg = "该申请单已被归档，不能禁用";
                            break;
                        case -200:
                            errMsg = "该申请单处于待审核状态，不能禁用";
                            break;
                        case -300:
                            errMsg = "该申请单处于未发布状态，不能禁用";
                            break;
                        case -400:
                            errMsg = "该申请单已被禁用，不能重复禁用";
                            break;
                        case -500:
                            errMsg = "该申请单最后一个履历处于未完成状态，不能禁用";
                            break;
                        case -1:
                            errMsg = "添加申请单履历失败";
                            break;
                        case -2:
                            errMsg = "更新申请单禁用状态失败";
                            break;
                        case -3:
                            errMsg = "更新车辆信息失败";
                            break;
                        default:
                            errMsg = "异常错误";
                            break;
                    }
                    ErrorLog e = new ErrorLog();
                    e.TargetIds = model.Car.ToString();
                    e.CodeTag = "ForbidInstance";
                    e.LogName = "禁用申请单";
                    e.ErrorMsg = errMsg;
                    ErrorLogService.AddErrorLog<Model.Instanceforbid>(e, model);
                    return false;
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                ErrorLog e = new ErrorLog();
                e.TargetIds = "0";
                e.CodeTag = "ForbidInstance";
                e.LogName = "禁用申请单";
                e.ErrorMsg = ex.Message.ToString();
                ErrorLogService.AddErrorLog<Model.Instanceforbid>(e, model);
                return false;
            }
        }

        /// <summary>
        /// 启用申请单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool EnableInstance(ref Model.Instanceenable model, out string errMsg)
        {
            errMsg = "";
            try
            {
                model.Status = model.Status.Trim();
                model.StartInfo = model.StartInfo.Trim();
                SqlParameter[] para = new SqlParameter[] 
                {
                    new SqlParameter("@Trace", model.Trace),
                    new SqlParameter("@Instance", model.Instance),
                    new SqlParameter("@Car", model.Car),
                    new SqlParameter("@Status", model.Status),
                    new SqlParameter("@StartInfo", model.StartInfo),
                    new SqlParameter("@Creater", model.Creater),
                    new SqlParameter("@CreatedDate", model.CreatedDate),
                    new SqlParameter("@OutState",SqlDbType.Int),
                    new SqlParameter("@return",SqlDbType.Int)
                };
                para[7].Direction = ParameterDirection.Output;
                para[8].Direction = ParameterDirection.ReturnValue;
                DBHelper.ExecuteNonQuery(CommandType.StoredProcedure, "proc_InstanceEnable", para);
                int outState = int.Parse(para[7].Value.ToString());
                int returnValue = int.Parse(para[8].Value.ToString());
                if (returnValue > 0)
                {
                    EventLog e = new EventLog();
                    e.TargetIds = returnValue.ToString();
                    e.CodeTag = "EnableInstance";
                    e.LogName = "启用申请单";
                    EventLogService.AddEventLog<Model.Instanceenable>(e, model);
                    return true;
                }
                else
                {
                    switch (outState)
                    {
                        case -100:
                            errMsg = "该申请单已被归档，不能启用";
                            break;
                        case -200:
                            errMsg = "该申请单已启用，不能重复启用";
                            break;
                        case -300:
                            errMsg = "该申请单最后一个履历处于未完成状态，不能禁用";
                            break;
                        case -1:
                            errMsg = "添加申请单履历失败";
                            break;
                        case -2:
                            errMsg = "更新申请单启用状态失败";
                            break;
                        case -3:
                            errMsg = "更新车辆信息失败";
                            break;
                        default:
                            errMsg = "异常错误";
                            break;
                    }
                    ErrorLog e = new ErrorLog();
                    e.TargetIds = model.Car.ToString();
                    e.CodeTag = "EnableInstance";
                    e.LogName = "启用申请单";
                    e.ErrorMsg = errMsg;
                    ErrorLogService.AddErrorLog<Model.Instanceenable>(e, model);
                    return false;
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                ErrorLog e = new ErrorLog();
                e.TargetIds = "0";
                e.CodeTag = "EnableInstance";
                e.LogName = "启用申请单";
                e.ErrorMsg = ex.Message.ToString();
                ErrorLogService.AddErrorLog<Model.Instanceenable>(e, model);
                return false;
            }
        }

        /// <summary>
        /// 中止申请单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AbortInstance(ref Model.Instanceabort model, out string errMsg)
        {
            errMsg = "";
            try
            {
                model.Status = model.Status.Trim();
                model.StartInfo = model.StartInfo.Trim();
                SqlParameter[] para = new SqlParameter[] 
                {
                    new SqlParameter("@Trace", model.Trace),
                    new SqlParameter("@Instance", model.Instance),
                    new SqlParameter("@Car", model.Car),
                    new SqlParameter("@Status", model.Status),
                    new SqlParameter("@StartInfo", model.StartInfo),
                    new SqlParameter("@Creater", model.Creater),
                    new SqlParameter("@CreatedDate", model.CreatedDate),
                    new SqlParameter("@OutState",SqlDbType.Int),
                    new SqlParameter("@return",SqlDbType.Int)
                };
                para[7].Direction = ParameterDirection.Output;
                para[8].Direction = ParameterDirection.ReturnValue;
                DBHelper.ExecuteNonQuery(CommandType.StoredProcedure, "proc_InstanceAbort", para);
                int outState = int.Parse(para[7].Value.ToString());
                int returnValue = int.Parse(para[8].Value.ToString());
                if (returnValue > 0)
                {
                    EventLog e = new EventLog();
                    e.TargetIds = returnValue.ToString();
                    e.CodeTag = "AbortInstance";
                    e.LogName = "中止申请单";
                    EventLogService.AddEventLog<Model.Instanceabort>(e, model);
                    return true;
                }
                else
                {
                    switch (outState)
                    {
                        case -100:
                            errMsg = "该申请单已被归档，不能中止";
                            break;
                        case -200:
                            errMsg = "该申请单处于待审核状态，不能中止";
                            break;
                        case -300:
                            errMsg = "该申请单处于未发布状态，不能中止";
                            break;
                        case -400:
                            errMsg = "该申请单已被禁用，不能中止";
                            break;
                        case -500:
                            errMsg = "该申请单最后一个履历处于未完成状态，不能中止";
                            break;
                        case -1:
                            errMsg = "添加申请单履历失败";
                            break;
                        case -2:
                            errMsg = "更新申请单中止状态失败";
                            break;
                        case -3:
                            errMsg = "更新车辆信息失败";
                            break;
                        default:
                            errMsg = "异常错误";
                            break;
                    }
                    ErrorLog e = new ErrorLog();
                    e.TargetIds = model.Car.ToString();
                    e.CodeTag = "AbortInstance";
                    e.LogName = "中止申请单";
                    e.ErrorMsg = errMsg;
                    ErrorLogService.AddErrorLog<Model.Instanceabort>(e, model);
                    return false;
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                ErrorLog e = new ErrorLog();
                e.TargetIds = "0";
                e.CodeTag = "AbortInstance";
                e.LogName = "中止申请单";
                e.ErrorMsg = ex.Message.ToString();
                ErrorLogService.AddErrorLog<Model.Instanceabort>(e, model);
                return false;
            }
        }

        /// <summary>
        /// 获取车辆申请单
        /// </summary>
        /// <param name="car"></param>
        /// <param name="count"></param>
        /// <param name="lastId"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static IList<Model.Instance> GetInstancesByCar(int car, int count, int lastId,out string errMsg)
        {
            errMsg = "";
            try
            {
                SqlParameter[] para = new SqlParameter[]
				{
					new SqlParameter("@Car", car),
					new SqlParameter("@Count", count),
					new SqlParameter("@LastId", lastId)
				};
                string lastIdWhere = "";
                if(lastId > 0){
                    lastIdWhere = " and Id < @LastId ";
                }
                string sql = string.Format("select top (@Count) * from [Instance] where Car = @Car and IsArchived = 1 {0} order by Id desc", lastIdWhere);
                DataTable dt = DBHelper.ExecuteGetDataTable(CommandType.Text, sql, para);

                IList<Model.Instance> listInstances = new List<Model.Instance>();
                foreach (DataRow dr in dt.Rows)
                {
                    Instance instance = new Instance();
                    instance.Id = (int)dr["Id"];
                    instance.Car = (int)dr["Car"];
                    instance.Project = (int)dr["Project"];
                    instance.Department = (int)dr["Department"];
                    instance.UserName = (string)dr["UserName"];
                    if (!DBNull.Value.Equals(dr["Oils"]))
                    {
                        string oilsStr = (string)dr["Oils"];
                        string[] oilsArrayTemp = oilsStr.Split(new char[]
						{
							','
						});
                        int[] oilsArray = Array.ConvertAll<string, int>(oilsArrayTemp, (string s) => int.Parse(s));
                        instance.Oils = oilsArray.ToList<int>();
                    }
                    instance.Goal = (string)dr["Goal"];
                    instance.StartDate = (DateTime)dr["StartDate"];
                    instance.EndDate = (DateTime)dr["EndDate"];
                    instance.IsReleased = (bool)dr["IsReleased"];
                    instance.IsPending = (bool)dr["IsPending"];
                    instance.IsArchived = (bool)dr["IsArchived"];
                    instance.IsEnable = (bool)dr["IsEnable"];
                    //if (DBNull.Value.Equals(dr["Message"]))
                    //{
                    //    instance.Message = null;
                    //}
                    //else
                    //{
                    //    instance.Message = new int?((int)dr["Message"]);
                    //}
                    instance.BillCount = (int)dr["BillCount"];
                    instance.Creater = (int)dr["Creater"];
                    instance.CreatedDate = (DateTime)dr["CreatedDate"];
                    instance.Modifier = (int)dr["Modifier"];
                    instance.ModifiedDate = (DateTime)dr["ModifiedDate"];
                    listInstances.Add(instance);
                }

                return listInstances;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 获取单个申请单
        /// </summary>
        /// <param name="id"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static Model.Instance GetInstanceById(int id, out string errMsg)
        {
            errMsg = "";
            try
            {
                string sql = "";
                SqlParameter[] para = new SqlParameter[]
				{
					new SqlParameter("@Id", id)
				};
                sql = "select * from [Instance] where Id = @Id";
                DataTable dt = DBHelper.ExecuteGetDataTable(CommandType.Text, sql, para);
                Model.Instance instance = null;
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    instance = new Instance();
                    instance.Id = (int)dr["Id"];
                    instance.Car = (int)dr["Car"];
                    instance.Project = (int)dr["Project"];
                    instance.Department = (int)dr["Department"];
                    instance.UserName = (string)dr["UserName"];
                    if (!DBNull.Value.Equals(dr["Oils"]))
                    {
                        string oilsStr = (string)dr["Oils"];
                        string[] oilsArrayTemp = oilsStr.Split(new char[]
						{
							','
						});
                        int[] oilsArray = Array.ConvertAll<string, int>(oilsArrayTemp, (string s) => int.Parse(s));
                        instance.Oils = oilsArray.ToList<int>();
                    }
                    instance.Goal = (string)dr["Goal"];
                    instance.StartDate = (DateTime)dr["StartDate"];
                    instance.EndDate = (DateTime)dr["EndDate"];
                    instance.IsReleased = (bool)dr["IsReleased"];
                    instance.IsPending = (bool)dr["IsPending"];
                    instance.IsArchived = (bool)dr["IsArchived"];
                    instance.IsEnable = (bool)dr["IsEnable"];
                    //if (DBNull.Value.Equals(dr["Message"]))
                    //{
                    //    instance.Message = null;
                    //}
                    //else
                    //{
                    //    instance.Message = new int?((int)dr["Message"]);
                    //}
                    instance.BillCount = (int)dr["BillCount"];
                    instance.Creater = (int)dr["Creater"];
                    instance.CreatedDate = (DateTime)dr["CreatedDate"];
                    instance.Modifier = (int)dr["Modifier"];
                    instance.ModifiedDate = (DateTime)dr["ModifiedDate"];
                }
                else
                {
                    errMsg = "该申请单不存在或已被删除";
                }
                return instance;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 一键优化
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool OneTouchOptimize(ref Model.Optimizer model, out string errMsg)
        {
            errMsg = "";
            try
            {
                SqlParameter[] para = new SqlParameter[] 
                {
                    new SqlParameter("@Creater", model.Creater),
                    new SqlParameter("@CreatedDate", model.CreatedDate),
                    new SqlParameter("@OutAllCarsCount",SqlDbType.Int),
                    new SqlParameter("@OutArchivedCarsCount",SqlDbType.Int),
                    new SqlParameter("@OutArchivedInstancesCount",SqlDbType.Int),
                    new SqlParameter("@OutState",SqlDbType.Int),
                    new SqlParameter("@return",SqlDbType.Int)
                };
                para[2].Direction = ParameterDirection.Output;
                para[3].Direction = ParameterDirection.Output;
                para[4].Direction = ParameterDirection.Output;
                para[5].Direction = ParameterDirection.Output;
                para[6].Direction = ParameterDirection.ReturnValue;
                DBHelper.ExecuteNonQuery(CommandType.StoredProcedure, "proc_OneTouchOptimize", para);
                int outAllCarsCount = int.Parse(para[2].Value.ToString());
                int outArchivedCarsCount = int.Parse(para[3].Value.ToString());
                int outArchivedInstancesCount = int.Parse(para[4].Value.ToString());
                int outState = int.Parse(para[5].Value.ToString());
                int returnValue = int.Parse(para[6].Value.ToString());
                if (returnValue > 0)
                {
                    model.AllCarsCount = outAllCarsCount;
                    model.ArchivedCarsCount = outArchivedCarsCount;
                    model.ArchivedInstancesCount = outArchivedInstancesCount;
                    EventLog e = new EventLog();
                    e.TargetIds = returnValue.ToString();
                    e.CodeTag = "OneTouchOptimize";
                    e.LogName = "一键优化";
                    EventLogService.AddEventLog<Model.Optimizer>(e, model);
                    return true;
                }
                else
                {
                    switch (outState)
                    {
                        case -1:
                            errMsg = "更新申请单履历归档状态失败";
                            break;
                        case -2:
                            errMsg = "更新申请单归档状态失败";
                            break;
                        case -3:
                            errMsg = "更新申请单关联车辆信息失败";
                            break;
                        case -11:
                            errMsg = "归档车辆失败";
                            break;
                        default:
                            errMsg = "异常错误";
                            break;
                    }
                    ErrorLog e = new ErrorLog();
                    e.TargetIds = "0";
                    e.CodeTag = "OneTouchOptimize";
                    e.LogName = "一键优化";
                    e.ErrorMsg = errMsg;
                    ErrorLogService.AddErrorLog<Model.Optimizer>(e, model);
                    return false;
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                ErrorLog e = new ErrorLog();
                e.TargetIds = "0";
                e.CodeTag = "OneTouchOptimize";
                e.LogName = "一键优化";
                e.ErrorMsg = ex.Message.ToString();
                ErrorLogService.AddErrorLog<Model.Optimizer>(e, model);
                return false;
            }
        }
    }
}
