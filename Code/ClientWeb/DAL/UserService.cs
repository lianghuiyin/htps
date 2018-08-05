using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Model;
using Utility;

namespace DAL
{
    /// <summary>
    /// 用户数据访问类
    /// </summary>
    public static class UserService
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <returns></returns>
        public static bool Login(ref Model.Login login, out string errMsg)
        {
            try
            {
                errMsg = "";
                string sql = "select Id,Name,Role,IsEnable from [User] where (Phone = @LogName and Password = @LogPassword) or (Email = @LogName and Password = @LogPassword)";
                SqlParameter[] para = new SqlParameter[]
                {
                    new SqlParameter("@LogName", login.LogName.Trim()),
                    new SqlParameter("@LogPassword", login.LogPassword)
                };
                DataTable dtUser = DBHelper.ExecuteGetDataTable(CommandType.Text, sql, para);
                if (dtUser.Rows.Count > 0)
                {
                    DataRow dr = dtUser.Rows[0];
                    if ((bool)dr["IsEnable"])
                    {
                        login.LogId = (int)dr["Id"];
                        login.IsPassed = true;

                        EventLog e = new EventLog();
                        e.TargetIds = login.LogId.ToString();
                        e.CodeTag = "Login";
                        e.LogName = "用户登录";
                        EventLogService.AddEventLog<Model.Login>(e, login);
                        return true;
                    }
                    else
                    {
                        errMsg = "该用户已被禁用";
                        login.IsPassed = false;
                        return false;
                    }
                }
                else
                {
                    errMsg = "用户名或密码不正确";
                    login.IsPassed = false;
                    return false;
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                ErrorLog e = new ErrorLog();
                e.TargetIds = login.LogId.ToString();
                e.CodeTag = "Login";
                e.LogName = "用户登录";
                e.ErrorMsg = ex.Message.ToString();
                ErrorLogService.AddErrorLog<Model.Login>(e, login);
                return false;
            }
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddUser(ref Model.User model, out string errMsg)
        {
            errMsg = "";
            try
            {
                model.Name = model.Name.Trim();
                model.Phone = model.Phone.Trim();
                model.Email = model.Email.Trim();
                model.Signature = model.Signature.Trim();
                SqlParameter[] para = new SqlParameter[] 
                {
                    new SqlParameter("@Name", model.Name),
                    new SqlParameter("@Phone", model.Phone),
                    new SqlParameter("@Email", model.Email),
                    new SqlParameter("@Role", model.Role),
                    new SqlParameter("@Signature", model.Signature),
                    new SqlParameter("@IsSignNeeded", model.IsSignNeeded),
                    new SqlParameter("@Creater", model.Creater),
                    new SqlParameter("@CreatedDate", model.CreatedDate),
                    new SqlParameter("@Modifier", model.Modifier),
                    new SqlParameter("@ModifiedDate", model.ModifiedDate),
                    new SqlParameter("@return",SqlDbType.Int)
                };
                para[10].Direction = ParameterDirection.ReturnValue;
                int i = DBHelper.ExecuteNonQuery(CommandType.StoredProcedure, "proc_UserInsert", para);
                if (i > 0)
                {
                    model.Id = int.Parse(para[10].Value.ToString());
                    EventLog e = new EventLog();
                    e.TargetIds = para[10].Value.ToString();
                    e.CodeTag = "AddUser";
                    e.LogName = "添加用户";
                    EventLogService.AddEventLog<Model.User>(e, model);
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
                e.CodeTag = "AddUser";
                e.LogName = "添加用户";
                e.ErrorMsg = ex.Message.ToString();
                ErrorLogService.AddErrorLog<Model.User>(e, model);
                return false;
            }
        }

        /// <summary>
        /// 根据id修改用户
        /// </summary>
        /// <param name="Model.User"></param>
        /// <returns></returns>
        public static bool ModifyUserById(ref Model.User model, out string errMsg)
        {
            errMsg = "";
            try
            {
                model.Name = model.Name.Trim();
                model.Phone = model.Phone.Trim();
                model.Email = model.Email.Trim();
                model.Signature = model.Signature.Trim();
                SqlParameter[] para = new SqlParameter[] 
                {
                    new SqlParameter("@Id", model.Id),
                    new SqlParameter("@Name", model.Name.Trim()),
                    new SqlParameter("@Phone", model.Phone.Trim()),
                    new SqlParameter("@Email", model.Email.Trim()),
                    new SqlParameter("@Role", model.Role),
                    new SqlParameter("@Signature", model.Signature.Trim()),
                    new SqlParameter("@IsSignNeeded", model.IsSignNeeded),
                    new SqlParameter("@IsEnable", model.IsEnable),
                    new SqlParameter("@Modifier", model.Modifier),
                    new SqlParameter("@ModifiedDate", model.ModifiedDate)
                };
                int i = DBHelper.ExecuteNonQuery(CommandType.StoredProcedure, "proc_UserByIdUpdate", para);
                if (i > 0)
                {
                    EventLog e = new EventLog();
                    e.TargetIds = model.Id.ToString();
                    e.CodeTag = "ModifyUserById";
                    e.LogName = "修改用户";
                    EventLogService.AddEventLog<Model.User>(e, model);
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
                e.CodeTag = "ModifyUserById";
                e.LogName = "修改用户";
                e.ErrorMsg = ex.Message.ToString();
                ErrorLogService.AddErrorLog<Model.User>(e, model);
                return false;
            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static bool DeleteUserByIds(string ids, out string errMsg)
        {
            errMsg = "";
            try
            {
                SqlParameter[] para = new SqlParameter[] 
                {
                    new SqlParameter("@model", "User"),
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
                        e.CodeTag = "DeleteUserByIds";
                        e.LogName = "删除用户";
                        EventLogService.AddEventLog<Model.User>(e, null);
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
                e.CodeTag = "DeleteUserByIds";
                e.LogName = "删除用户";
                e.ErrorMsg = ex.Message.ToString();
                ErrorLogService.AddErrorLog<Model.User>(e, null);
                return false;
            }
        }

        /// <summary>
        /// 检查是否重复
        /// </summary>
        /// <param name="Model.User"></param>
        /// <returns></returns>
        public static bool CheckRepeatForPhone(Model.User model)
        {
            try
            {
                string sql = "select * from [User] where len(Phone) > 0 and Phone = @Phone and Id <> @Id";
                SqlParameter[] para = new SqlParameter[]
                {
                    new SqlParameter("@Phone", model.Phone.Trim()),
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
                e.LogName = "检查是否重复-ForUserPhone";
                e.ErrorMsg = ex.Message.ToString();
                ErrorLogService.AddErrorLog<Model.User>(e, model);
                return true;
            }
        }

        /// <summary>
        /// 检查是否重复
        /// </summary>
        /// <param name="Model.User"></param>
        /// <returns></returns>
        public static bool CheckRepeatForEmail(Model.User model)
        {
            try
            {
                string sql = "select * from [User] where len(Email) > 0 and Email = @Email and Id <> @Id";
                SqlParameter[] para = new SqlParameter[]
                {
                    new SqlParameter("@Email", model.Email.Trim()),
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
                e.LogName = "检查是否重复-ForUserEmail";
                e.ErrorMsg = ex.Message.ToString();
                ErrorLogService.AddErrorLog<Model.User>(e, model);
                return true;
            }
        }

        /// <summary>
        /// 密码重置
        /// </summary>
        /// <param name="Model.User"></param>
        /// <returns></returns>
        public static bool Resetpwd(ref Model.Resetpwd model, out string errMsg)
        {
            errMsg = "";
            try
            {
                SqlParameter[] para = new SqlParameter[] 
                {
                    new SqlParameter("@User", model.User),
                    new SqlParameter("@NewPassword", model.NewPassword.Trim())
                };
                int i = DBHelper.ExecuteNonQuery(CommandType.StoredProcedure, "proc_UserResetpwd", para);
                if (i > 0)
                {
                    EventLog e = new EventLog();
                    e.TargetIds = model.User.ToString();
                    e.CodeTag = "Resetpwd";
                    e.LogName = "密码重置";
                    EventLogService.AddEventLog<Model.Resetpwd>(e, model);
                    return true;
                }
                else
                {
                    errMsg = "该记录已被删除，不能执行密码重置";
                    return false;
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                ErrorLog e = new ErrorLog();
                e.TargetIds = model.User.ToString();
                e.CodeTag = "Resetpwd";
                e.LogName = "密码重置";
                e.ErrorMsg = ex.Message.ToString();
                ErrorLogService.AddErrorLog<Model.Resetpwd>(e, model);
                return false;
            }
        }

        /// <summary>
        /// 修改账户密码
        /// </summary>
        /// <param name="Model.User"></param>
        /// <returns></returns>
        public static bool Updatepwd(ref Model.Accountpwd model, out string errMsg)
        {
            int outState = 0;
            errMsg = "";
            try
            {
                SqlParameter[] para = new SqlParameter[] 
                {
                    new SqlParameter("@User", model.User),
                    new SqlParameter("@OldPassword", model.OldPassword.Trim()),
                    new SqlParameter("@NewPassword", model.NewPassword.Trim()),
                    new SqlParameter("@OutState", outState)
                };
                para[3].Direction = ParameterDirection.Output;
                int i = DBHelper.ExecuteNonQuery(CommandType.StoredProcedure, "proc_AccountpwdUpdate", para);
                if (i > 0)
                {
                    EventLog e = new EventLog();
                    e.TargetIds = model.User.ToString();
                    e.CodeTag = "Updatepwd";
                    e.LogName = "修改账户密码";
                    EventLogService.AddEventLog<Model.Accountpwd>(e, model);
                    return true;
                }
                else
                {
                    outState = (int)para[3].Value;
                    if (outState == -1)
                    {
                        errMsg = "用户已被其他用户删除，不能修改密码";
                    }
                    else if (outState == -2)
                    {
                        errMsg = "密码错误";
                    }
                    else
                    {
                        errMsg = "未知错误";
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                ErrorLog e = new ErrorLog();
                e.TargetIds = model.User.ToString();
                e.CodeTag = "Updatepwd";
                e.LogName = "修改密码";
                e.ErrorMsg = ex.Message.ToString();
                ErrorLogService.AddErrorLog<Model.Accountpwd>(e, model);
                return false;
            }
        }
    }
}
