using Model;
using System;
using System.Data;
using System.Data.SqlClient;
using Utility;

namespace DAL
{
    /// <summary>
    /// 驾驶员签名数据访问类
    /// </summary>
    public static class SignatureService
    {
        /// <summary>
        /// 添加驾驶员签名
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddSignature(ref Signature model, out string errMsg)
		{
			errMsg = "";
			bool result;
			try
            {
                model.Name = model.Name.Trim();
				SqlParameter[] para = new SqlParameter[]
				{
					new SqlParameter("@Name", model.Name),
					new SqlParameter("@Sign", Convert.FromBase64String(model.Sign)),
					new SqlParameter("@Creater", model.Creater),
					new SqlParameter("@CreatedDate", model.CreatedDate),
					new SqlParameter("@return", SqlDbType.Int)
				};
                para[4].Direction = ParameterDirection.ReturnValue;
                int i = DBHelper.ExecuteNonQuery(CommandType.StoredProcedure, "proc_SignatureInsert", para);
                if (i > 0)
                {
                    model.Id = int.Parse(para[4].Value.ToString());
                    EventLog e = new EventLog();
                    e.TargetIds = para[4].Value.ToString();
                    e.CodeTag = "AddSignature";
                    e.LogName = "添加驾驶员签名";
                    EventLogService.AddEventLog<Model.Signature>(e, model);
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
				ErrorLogService.AddErrorLog<Signature>(new ErrorLog
				{
					TargetIds = "0",
                    CodeTag = "AddSignature",
                    LogName = "添加驾驶员签名",
					ErrorMsg = ex.Message.ToString()
				}, model);
				result = false;
			}
			return result;
		}

        /// <summary>
        /// 获取签字内容
        /// </summary>
        /// <param name="id"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static Model.Signature GetSignatureById(int id,out string errMsg)
        {
            errMsg = "";
            try
            {
                string sql = "";
                SqlParameter[] para = new SqlParameter[]
				{
					new SqlParameter("@Id", id)
				};
                sql = "select * from [Signature] where Id = @Id";
                DataTable dt = DBHelper.ExecuteGetDataTable(CommandType.Text, sql, para);
                Model.Signature signature = null;
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    string tempBase64String = Convert.ToBase64String((byte[])dr["Sign"]);
                    signature = new Signature
                    {
                        Id = (int)dr["Id"],
                        Name = (string)dr["Name"],
                        Sign = tempBase64String,
                        Creater = (int)dr["Creater"],
                        CreatedDate = (DateTime)dr["CreatedDate"]
                    };
                }
                else
                {
                    errMsg = "该签字不存在或已被删除";
                }
                return signature; ;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 删除签字
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static bool DeleteSignatureByIds(string ids, out string errMsg)
        {
            errMsg = "";
            try
            {
                SqlParameter[] para = new SqlParameter[] 
                {
                    new SqlParameter("@model", "Signature"),
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
                        e.CodeTag = "DeleteSignatureByIds";
                        e.LogName = "删除签字";
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
                e.CodeTag = "DeleteSignatureByIds";
                e.LogName = "删除签字";
                e.ErrorMsg = ex.Message.ToString();
                ErrorLogService.AddErrorLog<Model.Project>(e, null);
                return false;
            }
        }
	}
}
