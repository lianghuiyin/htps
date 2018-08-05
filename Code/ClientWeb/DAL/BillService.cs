using Model;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using Utility;

namespace DAL
{
	public static class BillService
	{
		public static bool AddBill(ref Bill model, out string errMsg)
		{
			errMsg = "";
			bool result;
			try
            {
                model.DriverName = model.DriverName.Trim();
				SqlParameter[] para = new SqlParameter[]
				{
					new SqlParameter("@Car", model.Car),
					new SqlParameter("@Instance", model.Instance),
					new SqlParameter("@Project", model.Project),
					new SqlParameter("@Department", model.Department),
					new SqlParameter("@Oil", model.Oil),
					new SqlParameter("@Volume", model.Volume),
					new SqlParameter("@Mileage", model.Mileage),
					new SqlParameter("@DriverName", model.DriverName),
					new SqlParameter("@Signature", model.Signature),
					new SqlParameter("@Rate", model.Rate),
					new SqlParameter("@Oiler", model.Oiler),
					new SqlParameter("@Time", model.Time),
					new SqlParameter("@IsLost", model.IsLost),
					new SqlParameter("@Creater", model.Creater),
					new SqlParameter("@CreatedDate", model.CreatedDate),
					new SqlParameter("@OutState", SqlDbType.Int),
					new SqlParameter("@return", SqlDbType.Int)
				};
				para[15].Direction = ParameterDirection.Output;
				para[16].Direction = ParameterDirection.ReturnValue;
				DBHelper.ExecuteNonQuery(CommandType.StoredProcedure, "proc_BillInsert", para);
                int outState = int.Parse(para[15].Value.ToString());
                int returnValue = int.Parse(para[16].Value.ToString());
				if (returnValue > 0)
				{
					model.Id = returnValue;
					EventLogService.AddEventLog<Bill>(new EventLog
					{
						TargetIds = returnValue.ToString(),
						CodeTag = "AddBill",
						LogName = "添加加油单"
					}, model);
					result = true;
				}
				else
                {
                    switch (outState)
                    {
                        case -100:
                            errMsg = "该车辆下对应加油申请单已结束，不能加油";
                            break;
                        case -200:
                            errMsg = "该车辆下对应加油申请单没有通过审核或被审核员中止，不能加油";
                            break;
                        case -300:
                            errMsg = "该车辆下对应加油申请单已被暂停，不能加油";
                            break;
                        case -400:
                            errMsg = "该车辆下对应加油申请单可加油时间不在当前时间范围，不能加油";
                            break;
                        case -500:
                            errMsg = "车辆里程数重复，不能重复提交相同里程数的加油单";
                            break;
                        case -1:
                            errMsg = "添加加油单失败";
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
                    e.CodeTag = "AddBill";
                    e.LogName = "添加加油单";
                    e.ErrorMsg = errMsg;
                    ErrorLogService.AddErrorLog<Model.Bill>(e, model);
					result = false;
				}
			}
			catch (Exception ex)
			{
				errMsg = ex.Message;
				ErrorLogService.AddErrorLog<Bill>(new ErrorLog
				{
					TargetIds = "0",
					CodeTag = "AddBill",
					LogName = "添加加油单",
					ErrorMsg = ex.Message.ToString()
				}, model);
				result = false;
			}
			return result;
		}

        /// <summary>
        /// 根据id修改加油单
        /// </summary>
        /// <param name="Model.Bill"></param>
        /// <returns></returns>
        public static bool ModifyBillById(ref Model.Bill model, out string errMsg)
        {
            errMsg = "";
            try
            {
                model.DriverName = model.DriverName.Trim();
                SqlParameter[] para = new SqlParameter[]
				{
                    new SqlParameter("@Id", model.Id),
					new SqlParameter("@Project", model.Project),
					new SqlParameter("@Department", model.Department),
					new SqlParameter("@Oil", model.Oil),
					new SqlParameter("@Volume", model.Volume),
					new SqlParameter("@Mileage", model.Mileage),
					new SqlParameter("@DriverName", model.DriverName),
					new SqlParameter("@Rate", model.Rate),
					new SqlParameter("@Time", model.Time),
					new SqlParameter("@Modifier", model.Modifier),
					new SqlParameter("@ModifiedDate", model.ModifiedDate)
				};
                int i = DBHelper.ExecuteNonQuery(CommandType.StoredProcedure, "proc_BillByIdUpdate", para);
                if (i > 0)
                {
                    EventLog e = new EventLog();
                    e.TargetIds = model.Id.ToString();
                    e.CodeTag = "ModifyBillById";
                    e.LogName = "修改加油单";
                    EventLogService.AddEventLog<Model.Bill>(e, model);
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
                e.CodeTag = "ModifyBillById";
                e.LogName = "修改加油单";
                e.ErrorMsg = ex.Message.ToString();
                ErrorLogService.AddErrorLog<Model.Bill>(e, model);
                return false;
            }
        }

        /// <summary>
        /// 获取加油单列表
        /// </summary>
        /// <param name="count">要返回的记录个数</param>
        /// <param name="billsFilter">筛选参数</param>
        /// <param name="lastId">最后（小）id值</param>
        /// <param name="errMsg">错误信息</param>
        /// <returns></returns>
        public static IList<Model.Bill> GetBills(
            int? count,
            BillsFilter billsFilter,
            out string errMsg)
        {
            errMsg = "";
            try
            {
                string sql = "";
                SqlParameter[] para = null;
                List<SqlParameter> listPara = new List<SqlParameter>();
                if (count == null) {
                    count = 20;
                }
                listPara.Add(new SqlParameter("@Count", count));
                string where = "";
                IList<string> listWhere = new List<string>();
                if (billsFilter.LastId > 0)
                {
                    listPara.Add(new SqlParameter("@LastId", billsFilter.LastId));
                    listWhere.Add("Id > @LastId");
                }
                if (billsFilter != null && billsFilter.Project > 0)
                {
                    listPara.Add(new SqlParameter("@Project", billsFilter.Project));
                    listWhere.Add("Project = @Project");
                }
                if (billsFilter != null && billsFilter.Department > 0)
                {
                    listPara.Add(new SqlParameter("@Department", billsFilter.Department));
                    listWhere.Add("Department = @Department");
                }
                if (billsFilter != null)
                {
                    listPara.Add(new SqlParameter("@StartDate", billsFilter.StartDate));
                    listPara.Add(new SqlParameter("@EndDate", billsFilter.EndDate));
                    listWhere.Add("Time between @StartDate and @EndDate");
                }
                para = listPara.ToArray();
                if (listWhere.Count > 0)
                {
                    where += "where ";
                    where += String.Join(" and ", listWhere.ToArray());
                }
                sql = string.Format("select top (@Count) * from [Bill] {0} order by Id asc", where);
                DataTable dt = DBHelper.ExecuteGetDataTable(CommandType.Text, sql, para);

                IList<Model.Bill> listBills = new List<Model.Bill>();
                foreach (DataRow dr in dt.Rows)
                {
                    Model.Bill bill = new Model.Bill();
                    bill.Id = (int)dr["Id"];
                    bill.Car = (int)dr["Car"];
                    bill.Instance = (int)dr["Instance"];
                    bill.Project = (int)dr["Project"];
                    bill.Department = (int)dr["Department"];
                    bill.Oil = (int)dr["Oil"];
                    bill.Volume = double.Parse(dr["Volume"].ToString());
                    bill.Mileage = double.Parse(dr["Mileage"].ToString());
                    bill.DriverName = (string)dr["DriverName"];
                    if (DBNull.Value.Equals(dr["Signature"]))
                    {
                        bill.Signature = null;
                    }
                    else
                    {
                        bill.Signature = new int?((int)dr["Signature"]);
                    }
                    if (DBNull.Value.Equals(dr["PreviousOil"]))
                    {
                        bill.PreviousOil = null;
                    }
                    else
                    {
                        bill.PreviousOil = new int?((int)dr["PreviousOil"]);
                    }
                    bill.Rate = double.Parse(dr["Rate"].ToString());
                    bill.Oiler = (int)dr["Oiler"];
                    bill.Time = (DateTime)dr["Time"];
                    bill.IsLost = (bool)dr["IsLost"];
                    bill.IsPrinted = (bool)dr["IsPrinted"];
                    bill.Creater = (int)dr["Creater"];
                    bill.CreatedDate = (DateTime)dr["CreatedDate"];
                    bill.Modifier = (int)dr["Modifier"];
                    bill.ModifiedDate = (DateTime)dr["ModifiedDate"];
                    listBills.Add(bill);
                }

                return listBills;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 根据时间获取加油单列表
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static IList<Model.Bill> GetBillsByTime(DateTime startDate, DateTime endDate, out string errMsg)
        {
            errMsg = "";
            try
            {
                SqlParameter[] para = new SqlParameter[]
				{
					new SqlParameter("@StartDate", startDate),
					new SqlParameter("@EndDate", endDate)
				};
                string sql = "select * from [Bill] where Time between @StartDate and @EndDate order by Id asc";
                DataTable dt = DBHelper.ExecuteGetDataTable(CommandType.Text, sql, para);

                IList<Model.Bill> listBills = new List<Model.Bill>();
                foreach (DataRow dr in dt.Rows)
                {
                    Model.Bill bill = new Model.Bill();
                    bill.Id = (int)dr["Id"];
                    bill.Car = (int)dr["Car"];
                    bill.Instance = (int)dr["Instance"];
                    bill.Project = (int)dr["Project"];
                    bill.Department = (int)dr["Department"];
                    bill.Oil = (int)dr["Oil"];
                    bill.Volume = double.Parse(dr["Volume"].ToString());
                    bill.Mileage = double.Parse(dr["Mileage"].ToString());
                    bill.DriverName = (string)dr["DriverName"];
                    if (DBNull.Value.Equals(dr["Signature"]))
                    {
                        bill.Signature = null;
                    }
                    else
                    {
                        bill.Signature = new int?((int)dr["Signature"]);
                    }
                    if (DBNull.Value.Equals(dr["PreviousOil"]))
                    {
                        bill.PreviousOil = null;
                    }
                    else
                    {
                        bill.PreviousOil = new int?((int)dr["PreviousOil"]);
                    }
                    bill.Rate = double.Parse(dr["Rate"].ToString());
                    bill.Oiler = (int)dr["Oiler"];
                    bill.Time = (DateTime)dr["Time"];
                    bill.IsLost = (bool)dr["IsLost"];
                    bill.IsPrinted = (bool)dr["IsPrinted"];
                    bill.Creater = (int)dr["Creater"];
                    bill.CreatedDate = (DateTime)dr["CreatedDate"];
                    bill.Modifier = (int)dr["Modifier"];
                    bill.ModifiedDate = (DateTime)dr["ModifiedDate"];
                    listBills.Add(bill);
                }

                return listBills;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 获取指定申请单车辆上一次加油单
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static IList<Model.Bill> GetLastOneBillByCar(int id,int car, out string errMsg)
        {
            errMsg = "";
            try
            {
                SqlParameter[] para = new SqlParameter[]
				{
					new SqlParameter("@Id", id),
					new SqlParameter("@Car", car)
				};
                string sql = "select top 1 * from [Bill] where Id < @Id and Car = @Car order by Id desc";
                DataTable dt = DBHelper.ExecuteGetDataTable(CommandType.Text, sql, para);

                IList<Model.Bill> listBills = new List<Model.Bill>();
                foreach (DataRow dr in dt.Rows)
                {
                    Model.Bill bill = new Model.Bill();
                    bill.Id = (int)dr["Id"];
                    bill.Car = (int)dr["Car"];
                    bill.Instance = (int)dr["Instance"];
                    bill.Project = (int)dr["Project"];
                    bill.Department = (int)dr["Department"];
                    bill.Oil = (int)dr["Oil"];
                    bill.Volume = double.Parse(dr["Volume"].ToString());
                    bill.Mileage = double.Parse(dr["Mileage"].ToString());
                    bill.DriverName = (string)dr["DriverName"];
                    if (DBNull.Value.Equals(dr["Signature"]))
                    {
                        bill.Signature = null;
                    }
                    else
                    {
                        bill.Signature = new int?((int)dr["Signature"]);
                    }
                    if (DBNull.Value.Equals(dr["PreviousOil"]))
                    {
                        bill.PreviousOil = null;
                    }
                    else
                    {
                        bill.PreviousOil = new int?((int)dr["PreviousOil"]);
                    }
                    bill.Rate = double.Parse(dr["Rate"].ToString());
                    bill.Oiler = (int)dr["Oiler"];
                    bill.Time = (DateTime)dr["Time"];
                    bill.IsLost = (bool)dr["IsLost"];
                    bill.IsPrinted = (bool)dr["IsPrinted"];
                    bill.Creater = (int)dr["Creater"];
                    bill.CreatedDate = (DateTime)dr["CreatedDate"];
                    bill.Modifier = (int)dr["Modifier"];
                    bill.ModifiedDate = (DateTime)dr["ModifiedDate"];
                    listBills.Add(bill);
                }

                return listBills;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 获取单个加油单
        /// </summary>
        /// <param name="id"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static Model.Bill GetBillById(int id, out string errMsg)
        {
            errMsg = "";
            try
            {
                string sql = "";
                SqlParameter[] para = new SqlParameter[]
				{
					new SqlParameter("@Id", id)
				};
                sql = "select * from [Bill] where Id = @Id";
                DataTable dt = DBHelper.ExecuteGetDataTable(CommandType.Text, sql, para);
                Model.Bill bill = null;
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    bill = new Bill();
                    bill.Id = (int)dr["Id"];
                    bill.Car = (int)dr["Car"];
                    bill.Instance = (int)dr["Instance"];
                    bill.Project = (int)dr["Project"];
                    bill.Department = (int)dr["Department"];
                    bill.Oil = (int)dr["Oil"];
                    bill.Volume = double.Parse(dr["Volume"].ToString());
                    bill.Mileage = double.Parse(dr["Mileage"].ToString());
                    bill.DriverName = (string)dr["DriverName"];
                    if (DBNull.Value.Equals(dr["Signature"]))
                    {
                        bill.Signature = null;
                    }
                    else
                    {
                        bill.Signature = new int?((int)dr["Signature"]);
                    }
                    if (DBNull.Value.Equals(dr["PreviousOil"]))
                    {
                        bill.PreviousOil = null;
                    }
                    else
                    {
                        bill.PreviousOil = new int?((int)dr["PreviousOil"]);
                    }
                    bill.Rate = double.Parse(dr["Rate"].ToString());
                    bill.Oiler = (int)dr["Oiler"];
                    bill.Time = (DateTime)dr["Time"];
                    bill.IsLost = (bool)dr["IsLost"];
                    bill.IsPrinted = (bool)dr["IsPrinted"];
                    bill.Creater = (int)dr["Creater"];
                    bill.CreatedDate = (DateTime)dr["CreatedDate"];
                    bill.Modifier = (int)dr["Modifier"];
                    bill.ModifiedDate = (DateTime)dr["ModifiedDate"];
                }
                else
                {
                    errMsg = "该加油单不存在或已被删除";
                }
                return bill;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 获取加油单报表
        /// </summary>
        /// <param name="billsFilter">筛选参数</param>
        /// <param name="errMsg">错误信息</param>
        /// <returns></returns>
        public static IList<Model.Report> GetReports(
            ReportsFilter reportsFilter,
            out string errMsg)
        {
            errMsg = "";
            try
            {
                string sql = "";
                SqlParameter[] para = null;
                List<SqlParameter> listPara = new List<SqlParameter>();
                string where = "";
                IList<string> listWhere = new List<string>();
                if (reportsFilter != null && reportsFilter.Project > 0)
                {
                    listPara.Add(new SqlParameter("@Project", reportsFilter.Project));
                    listWhere.Add("Project = @Project");
                }
                if (reportsFilter != null && reportsFilter.Department > 0)
                {
                    listPara.Add(new SqlParameter("@Department", reportsFilter.Department));
                    listWhere.Add("Department = @Department");
                }
                if (reportsFilter != null)
                {
                    listPara.Add(new SqlParameter("@StartDate", reportsFilter.StartDate));
                    listPara.Add(new SqlParameter("@EndDate", reportsFilter.EndDate));
                    listWhere.Add("Time between @StartDate and @EndDate");
                }
                para = listPara.ToArray();
                if (listWhere.Count > 0)
                {
                    where += "where ";
                    where += String.Join(" and ", listWhere.ToArray());
                }
                sql = string.Format("select convert(int,ROW_NUMBER() over(order by Project)) as Id,sum(Volume) as Volume,Project,Department,Oil,getdate() as CreatedDate from [Bill] {0} group by Project,Department,Oil", where);
                DataTable dt = DBHelper.ExecuteGetDataTable(CommandType.Text, sql, para);

                IList<Model.Report> listReports = new List<Model.Report>();
                foreach (DataRow dr in dt.Rows)
                {
                    Model.Report report = new Model.Report();
                    report.Id = (int)dr["Id"];
                    report.Project = (int)dr["Project"];
                    report.Department = (int)dr["Department"];
                    report.Oil = (int)dr["Oil"];
                    report.Volume = double.Parse(dr["Volume"].ToString());
                    report.CreatedDate = (DateTime)dr["CreatedDate"];
                    listReports.Add(report);
                }

                return listReports;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 获取加油单小票打印内容
        /// </summary>
        /// <param name="lastId"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static List<Model.BillPrinter> GetBillsForPrinter(int lastId, out string errMsg)
        {
            errMsg = "";
            try
            {
                SqlParameter[] para = new SqlParameter[]
				{
					new SqlParameter("@LastId", lastId)
				};
                DataTable dt = DBHelper.ExecuteGetDataTable(CommandType.StoredProcedure, "proc_BillsForPrinterSelect", para);

                List<Model.BillPrinter> listBills = new List<Model.BillPrinter>();
                foreach (DataRow dr in dt.Rows)
                {
                    Model.BillPrinter bill = new Model.BillPrinter();
                    bill.Id = (int)dr["Id"];
                    bill.CarNumber = (string)dr["CarNumber"];
                    bill.CarVin = (string)dr["CarVin"];
                    bill.ProjectName = (string)dr["ProjectName"];
                    bill.DepartmentName = (string)dr["DepartmentName"];
                    bill.OilName = (string)dr["OilName"];
                    bill.Volume = double.Parse(dr["Volume"].ToString());
                    bill.Mileage = double.Parse(dr["Mileage"].ToString());
                    bill.DriverName = (string)dr["DriverName"];
                    bill.Rate = double.Parse(dr["Rate"].ToString());
                    bill.OilerName = (string)dr["OilerName"];
                    //bill.CreaterName = (string)dr["CreaterName"];
                    bill.Time = (DateTime)dr["Time"];
                    //bill.CreatedDate = (DateTime)dr["CreatedDate"];
                    bill.IsLost = (bool)dr["IsLost"];
                    bill.IsPrinted = (bool)dr["IsPrinted"];
                    listBills.Add(bill);
                }

                return listBills;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 获取最新加油单Id号
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static int GetLastBillId(out string errMsg)
        {
            errMsg = "";
            int re = -1;
            try
            {
                string sql = "select top 1 Id from [Bill] order by Id desc";
                object lastId = DBHelper.ExecuteScalar(CommandType.Text, sql);
                if (lastId == null)
                {
                    re = 0;
                }
                else
                {
                    re = (int)lastId;
                }
                return re;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// 修改加油单打印状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static bool ModifyBillPrintedById(int id,out string errMsg)
        {
            errMsg = "";
            try
            {
                string sql = "update [Bill] set IsPrinted = 1,ModifiedDate = @Now where Id = @Id";
                SqlParameter[] para = new SqlParameter[]
				{
					new SqlParameter("@Id", id),
					new SqlParameter("@Now", DateTime.Now)
				};
                int i = DBHelper.ExecuteNonQuery(CommandType.Text, sql, para);
                if (i > 0)
                {
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
                return false;
            }
        }

        /// <summary>
        /// 获取加油单导出列表
        /// </summary>
        /// <param name="count">要返回的记录个数</param>
        /// <param name="billsFilter">筛选参数</param>
        /// <param name="lastId">最后（小）id值</param>
        /// <param name="errMsg">错误信息</param>
        /// <returns></returns>
        public static IList<Model.BillPrinter> GetBillsForExport(
            int? count,
            BillsFilter billsFilter,
            out string errMsg)
        {
            errMsg = "";
            try
            {
                string sql = "";
                SqlParameter[] para = null;
                List<SqlParameter> listPara = new List<SqlParameter>();
                if (count == null)
                {
                    count = 2000;
                }
                listPara.Add(new SqlParameter("@Count", count));
                string where = "";
                IList<string> listWhere = new List<string>();
                if (billsFilter.LastId > 0)
                {
                    listPara.Add(new SqlParameter("@LastId", billsFilter.LastId));
                    listWhere.Add("Id > @LastId");
                }
                if (billsFilter != null && billsFilter.Project > 0)
                {
                    listPara.Add(new SqlParameter("@Project", billsFilter.Project));
                    listWhere.Add("Project = @Project");
                }
                if (billsFilter != null && billsFilter.Department > 0)
                {
                    listPara.Add(new SqlParameter("@Department", billsFilter.Department));
                    listWhere.Add("Department = @Department");
                }
                if (billsFilter != null)
                {
                    listPara.Add(new SqlParameter("@StartDate", billsFilter.StartDate));
                    listPara.Add(new SqlParameter("@EndDate", billsFilter.EndDate));
                    listWhere.Add("Time between @StartDate and @EndDate");
                }
                para = listPara.ToArray();
                if (listWhere.Count > 0)
                {
                    where += "where ";
                    where += String.Join(" and ", listWhere.ToArray());
                }
                sql = string.Format(@"select top (@Count) Id,(select Number from Car where Car.Id = Bill.Car) as CarNumber,
		(select Vin from Car where Car.Id = Bill.Car) as CarVin,
		(select [Name] from Project where Project.Id = Bill.Project) as ProjectName,
		(select [Name] from Department where Department.Id = Bill.Department) as DepartmentName,
		(select [Name] from Oil where Oil.Id = Bill.Oil) as OilName,
		Volume,Mileage,DriverName,Rate,
		(select [Name] from [User] where [User].Id = Bill.Oiler) as OilerName,
		(select [UserName] from Instance where [Instance].Id = Bill.Instance) as UserName,
		(select [Name] from [User] where [User].Id = (select [Creater] from Instance where [Instance].Id = Bill.Instance)) as ApplicantName,
		[Time],IsLost,IsPrinted from [Bill] {0} order by Id asc", where);
                DataTable dt = DBHelper.ExecuteGetDataTable(CommandType.Text, sql, para);

                IList<Model.BillPrinter> listBills = new List<Model.BillPrinter>();
                foreach (DataRow dr in dt.Rows)
                {
                    Model.BillPrinter bill = new Model.BillPrinter();
                    bill.Id = (int)dr["Id"];
                    bill.CarNumber = (string)dr["CarNumber"];
                    bill.CarVin = (string)dr["CarVin"];
                    bill.ProjectName = (string)dr["ProjectName"];
                    bill.DepartmentName = (string)dr["DepartmentName"];
                    bill.OilName = (string)dr["OilName"];
                    bill.UserName = (string)dr["UserName"];
                    bill.ApplicantName = (string)dr["ApplicantName"];
                    bill.Volume = double.Parse(dr["Volume"].ToString());
                    bill.Mileage = double.Parse(dr["Mileage"].ToString());
                    bill.DriverName = (string)dr["DriverName"];
                    bill.Rate = double.Parse(dr["Rate"].ToString());
                    bill.OilerName = (string)dr["OilerName"];
                    //bill.CreaterName = (string)dr["CreaterName"];
                    bill.Time = (DateTime)dr["Time"];
                    //bill.CreatedDate = (DateTime)dr["CreatedDate"];
                    bill.IsLost = (bool)dr["IsLost"];
                    bill.IsPrinted = (bool)dr["IsPrinted"];
                    listBills.Add(bill);
                }

                return listBills;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 获取加油单导出列表加油单个数
        /// </summary>
        /// <param name="count"></param>
        /// <param name="billsFilter"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static int GetBillsCountForExport(
            BillsFilter billsFilter,
            out string errMsg)
        {
            errMsg = "";
            try
            {
                string sql = "";
                SqlParameter[] para = null;
                List<SqlParameter> listPara = new List<SqlParameter>();
                string where = "";
                IList<string> listWhere = new List<string>();
                if (billsFilter != null && billsFilter.Project > 0)
                {
                    listPara.Add(new SqlParameter("@Project", billsFilter.Project));
                    listWhere.Add("Project = @Project");
                }
                if (billsFilter != null && billsFilter.Department > 0)
                {
                    listPara.Add(new SqlParameter("@Department", billsFilter.Department));
                    listWhere.Add("Department = @Department");
                }
                if (billsFilter != null)
                {
                    listPara.Add(new SqlParameter("@StartDate", billsFilter.StartDate));
                    listPara.Add(new SqlParameter("@EndDate", billsFilter.EndDate));
                    listWhere.Add("Time between @StartDate and @EndDate");
                }
                para = listPara.ToArray();
                if (listWhere.Count > 0)
                {
                    where += "where ";
                    where += String.Join(" and ", listWhere.ToArray());
                }
                sql = string.Format(@"select count(*) from [Bill] {0}", where);
                Object reCount = DBHelper.ExecuteScalar(CommandType.Text, sql, para);
                return (int)reCount;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return -1;
            }
        }
	}
}
