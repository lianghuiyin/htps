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
    /// 申请单履历数据访问类
    /// </summary>
    public static class TraceService
    {
        /// <summary>
        /// 获取车辆申请单履历
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static IList<Model.Trace> GetTracesByInstance(int instance, out string errMsg)
        {
            errMsg = "";
            try
            {
                SqlParameter[] para = new SqlParameter[]
				{
					new SqlParameter("@Instance", instance)
				};
                string sql = "select * from [Trace] where Instance = @Instance order by Id desc";
                DataTable dt = DBHelper.ExecuteGetDataTable(CommandType.Text, sql, para);

                IList<Model.Trace> listTraces = new List<Model.Trace>();
                foreach (DataRow dr in dt.Rows)
                {
                    Trace trace = new Trace();
                    trace.Id = (int)dr["Id"];
                    trace.Car = (int)dr["Car"];
                    trace.Instance = (int)dr["Instance"];
                    if (DBNull.Value.Equals(dr["PreviousTrace"]))
                    {
                        trace.PreviousTrace = null;
                    }
                    else
                    {
                        trace.PreviousTrace = new int?((int)dr["PreviousTrace"]);
                    }
                    trace.Status = (string)dr["Status"];
                    trace.IsFinished = (bool)dr["IsFinished"];
                    trace.IsArchived = (bool)dr["IsArchived"];
                    trace.Project = (int)dr["Project"];
                    trace.Department = (int)dr["Department"];
                    trace.UserName = (string)dr["UserName"];
                    if (!DBNull.Value.Equals(dr["Oils"]))
                    {
                        string oilsStr = (string)dr["Oils"];
                        string[] oilsArrayTemp = oilsStr.Split(new char[]
						{
							','
						});
                        int[] oilsArray = Array.ConvertAll<string, int>(oilsArrayTemp, (string s) => int.Parse(s));
                        trace.Oils = oilsArray.ToList<int>();
                    }
                    trace.Goal = (string)dr["Goal"];
                    trace.StartDate = (DateTime)dr["StartDate"];
                    trace.EndDate = (DateTime)dr["EndDate"];
                    trace.StartInfo = (string)dr["StartInfo"];
                    trace.EndInfo = (string)dr["EndInfo"];
                    trace.Creater = (int)dr["Creater"];
                    trace.CreatedDate = (DateTime)dr["CreatedDate"];
                    trace.Modifier = (int)dr["Modifier"];
                    trace.ModifiedDate = (DateTime)dr["ModifiedDate"];
                    listTraces.Add(trace);
                }

                return listTraces;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return null;
            }
        }
    }
}
