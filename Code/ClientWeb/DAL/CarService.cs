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
    /// 车辆数据访问类
    /// </summary>
    public static class CarService
    {
        /// <summary>
        /// 添加车辆
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddCar(ref Model.Car model, out string errMsg)
        {
            errMsg = "";
            try
            {
                model.Number = model.Number.Trim();
                model.Vin = model.Vin.Trim();
                model.Model = model.Model.Trim();
                model.Description = model.Description.Trim();
                SqlParameter[] para = new SqlParameter[] 
                {
                    new SqlParameter("@Number", model.Number),
                    new SqlParameter("@Vin", model.Vin),
                    new SqlParameter("@Model", model.Model),
                    new SqlParameter("@Description", model.Description),
                    new SqlParameter("@Creater", model.Creater),
                    new SqlParameter("@CreatedDate", model.CreatedDate),
                    new SqlParameter("@Modifier", model.Modifier),
                    new SqlParameter("@ModifiedDate", model.ModifiedDate),
                    new SqlParameter("@return",SqlDbType.Int)
                };
                para[8].Direction = ParameterDirection.ReturnValue;
                int i = DBHelper.ExecuteNonQuery(CommandType.StoredProcedure, "proc_CarInsert", para);
                if (i > 0)
                {
                    model.Id = int.Parse(para[8].Value.ToString());
                    EventLog e = new EventLog();
                    e.TargetIds = para[8].Value.ToString();
                    e.CodeTag = "AddCar";
                    e.LogName = "添加车辆";
                    EventLogService.AddEventLog<Model.Car>(e, model);
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
                e.CodeTag = "AddCar";
                e.LogName = "添加车辆";
                e.ErrorMsg = ex.Message.ToString();
                ErrorLogService.AddErrorLog<Model.Car>(e, model);
                return false;
            }
        }

        /// <summary>
        /// 根据id修改车辆
        /// </summary>
        /// <param name="Model.Car"></param>
        /// <returns></returns>
        public static bool ModifyCarById(ref Model.Car model, out string errMsg)
        {
            errMsg = "";
            try
            {
                model.Number = model.Number.Trim();
                model.Vin = model.Vin.Trim();
                model.Model = model.Model.Trim();
                model.Description = model.Description.Trim();
                SqlParameter[] para = new SqlParameter[] 
                {
                    new SqlParameter("@Id", model.Id),
                    new SqlParameter("@Number", model.Number),
                    new SqlParameter("@Vin", model.Vin),
                    new SqlParameter("@Model", model.Model),
                    new SqlParameter("@Description", model.Description),
                    new SqlParameter("@Modifier", model.Modifier),
                    new SqlParameter("@ModifiedDate", model.ModifiedDate)
                };
                int i = DBHelper.ExecuteNonQuery(CommandType.StoredProcedure, "proc_CarByIdUpdate", para);
                if (i > 0)
                {
                    EventLog e = new EventLog();
                    e.TargetIds = model.Id.ToString();
                    e.CodeTag = "ModifyCarById";
                    e.LogName = "修改车辆";
                    EventLogService.AddEventLog<Model.Car>(e, model);
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
                e.CodeTag = "ModifyCarById";
                e.LogName = "修改车辆";
                e.ErrorMsg = ex.Message.ToString();
                ErrorLogService.AddErrorLog<Model.Car>(e, model);
                return false;
            }
        }

        /// <summary>
        /// 删除车辆
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static bool DeleteCarByIds(string ids, out string errMsg)
        {
            errMsg = "";
            try
            {
                SqlParameter[] para = new SqlParameter[] 
                {
                    new SqlParameter("@model", "Car"),
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
                        e.CodeTag = "DeleteCarByIds";
                        e.LogName = "删除车辆";
                        EventLogService.AddEventLog<Model.Car>(e, null);
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
                e.CodeTag = "DeleteCarByIds";
                e.LogName = "删除车辆";
                e.ErrorMsg = ex.Message.ToString();
                ErrorLogService.AddErrorLog<Model.Car>(e,null);
                return false;
            }
        }

        /// <summary>
        /// 检查是否重复
        /// </summary>
        /// <param name="Model.Car"></param>
        /// <returns></returns>
        public static bool CheckRepeatForNumber(Model.Car model)
        {
            try
            {
                string sql = "select * from Car where Number = @Number and Id <> @Id";
                SqlParameter[] para = new SqlParameter[]
                {
                    new SqlParameter("@Number", model.Number.Trim()),
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
                e.LogName = "检查是否重复-ForCarNumber";
                e.ErrorMsg = ex.Message.ToString();
                ErrorLogService.AddErrorLog<Model.Car>(e, model);
                return true;
            }
        }

        /// <summary>
        /// 检查是否重复
        /// </summary>
        /// <param name="Model.Car"></param>
        /// <returns></returns>
        public static bool CheckRepeatForVin(Model.Car model)
        {
            try
            {
                string sql = "select * from Car where Vin = @Vin and Id <> @Id";
                SqlParameter[] para = new SqlParameter[]
                {
                    new SqlParameter("@Vin", model.Vin.Trim()),
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
                e.LogName = "检查是否重复-ForCarVin";
                e.ErrorMsg = ex.Message.ToString();
                ErrorLogService.AddErrorLog<Model.Car>(e, model);
                return true;
            }
        }

        /// <summary>
        /// 车辆归档
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool ArchiveCar(ref Model.Cararchive model, out string errMsg)
        {
            errMsg = "";
            try
            {
                SqlParameter[] para = new SqlParameter[] 
                {
                    new SqlParameter("@Car", model.Car),
                    new SqlParameter("@Creater", model.Creater),
                    new SqlParameter("@CreatedDate", model.CreatedDate),
                    new SqlParameter("@OutState",SqlDbType.Int),
                    new SqlParameter("@return",SqlDbType.Int)
                };
                para[3].Direction = ParameterDirection.Output;
                para[4].Direction = ParameterDirection.ReturnValue;
                DBHelper.ExecuteNonQuery(CommandType.StoredProcedure, "proc_CarArchive", para);
                int outState = int.Parse(para[3].Value.ToString());
                int returnValue = int.Parse(para[4].Value.ToString());
                if (returnValue > 0)
                {
                    EventLog e = new EventLog();
                    e.TargetIds = returnValue.ToString();
                    e.CodeTag = "ArchiveCar";
                    e.LogName = "车辆归档";
                    EventLogService.AddEventLog<Model.Cararchive>(e, model);
                    return true;
                }
                else
                {
                    switch (outState)
                    {
                        case -100:
                            errMsg = "该车辆下有申请单没有归档，不能归档该车辆";
                            break;
                        case -1:
                            errMsg = "归档车辆失败";
                            break;
                        default:
                            errMsg = "异常错误";
                            break;
                    }
                    ErrorLog e = new ErrorLog();
                    e.TargetIds = model.Car.ToString();
                    e.CodeTag = "ArchiveCar";
                    e.LogName = "车辆归档";
                    e.ErrorMsg = errMsg;
                    ErrorLogService.AddErrorLog<Model.Cararchive>(e, model);
                    return false;
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                ErrorLog e = new ErrorLog();
                e.TargetIds = "0";
                e.CodeTag = "ArchiveCar";
                e.LogName = "车辆归档";
                e.ErrorMsg = ex.Message.ToString();
                ErrorLogService.AddErrorLog<Model.Cararchive>(e, model);
                return false;
            }
        }

        /// <summary>
        /// 车辆还原
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool RestoreCar(ref Model.Carrestore model, out string errMsg)
        {
            errMsg = "";
            try
            {
                SqlParameter[] para = new SqlParameter[] 
                {
                    new SqlParameter("@Car", model.Car),
                    new SqlParameter("@Creater", model.Creater),
                    new SqlParameter("@CreatedDate", model.CreatedDate),
                    new SqlParameter("@OutState",SqlDbType.Int),
                    new SqlParameter("@return",SqlDbType.Int)
                };
                para[3].Direction = ParameterDirection.Output;
                para[4].Direction = ParameterDirection.ReturnValue;
                DBHelper.ExecuteNonQuery(CommandType.StoredProcedure, "proc_CarRestore", para);
                int outState = int.Parse(para[3].Value.ToString());
                int returnValue = int.Parse(para[4].Value.ToString());
                if (returnValue > 0)
                {
                    EventLog e = new EventLog();
                    e.TargetIds = returnValue.ToString();
                    e.CodeTag = "RestoreCar";
                    e.LogName = "车辆还原";
                    EventLogService.AddEventLog<Model.Carrestore>(e, model);
                    return true;
                }
                else
                {
                    switch (outState)
                    {
                        case -100:
                            errMsg = "该车辆下有申请单没有归档，不能归档该车辆";
                            break;
                        case -1:
                            errMsg = "归档车辆失败";
                            break;
                        default:
                            errMsg = "异常错误";
                            break;
                    }
                    ErrorLog e = new ErrorLog();
                    e.TargetIds = model.Car.ToString();
                    e.CodeTag = "RestoreCar";
                    e.LogName = "车辆还原";
                    e.ErrorMsg = errMsg;
                    ErrorLogService.AddErrorLog<Model.Carrestore>(e, model);
                    return false;
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                ErrorLog e = new ErrorLog();
                e.TargetIds = "0";
                e.CodeTag = "RestoreCar";
                e.LogName = "车辆还原";
                e.ErrorMsg = ex.Message.ToString();
                ErrorLogService.AddErrorLog<Model.Carrestore>(e, model);
                return false;
            }
        }

        /// <summary>
        /// 获取已归档车辆
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <param name="lastId"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static IList<Model.Car> GetArchivedCars(string key, int count, int lastId, out string errMsg)
        {
            errMsg = "";
            try
            {
                if (key == null)
                {
                    key = "";
                }
                else {
                    key = string.Format("%{0}%", key);
                }
                SqlParameter[] para = new SqlParameter[]
				{
					new SqlParameter("@Key", (key == null ? "" : key)),
					new SqlParameter("@Count", count),
					new SqlParameter("@LastId", lastId)
				};
                string keyWhere = "";
                if (key != null && key.Length > 0)
                {
                    keyWhere = " (Number like @Key or Vin like @Key or Model like @Key) and ";
                }
                string lastIdWhere = "";
                if (lastId > 0)
                {
                    lastIdWhere = " and Id < @LastId ";
                }
                string sql = string.Format("select top (@Count) * from [Car] where {0} IsArchived = 1 {1} order by Id desc",keyWhere, lastIdWhere);
                DataTable dt = DBHelper.ExecuteGetDataTable(CommandType.Text, sql, para);

                IList<Model.Car> listCars = new List<Model.Car>();
                foreach (DataRow dr in dt.Rows)
                {
                    Car car = new Car();
                    car.Id = (int)dr["Id"];
                    car.Number = (string)dr["Number"];
                    car.Vin = (string)dr["Vin"];
                    car.Model = (string)dr["Model"];
                    car.IsArchived = (bool)dr["IsArchived"];
                    car.InstanceCount = (int)dr["InstanceCount"];
                    car.BillCount = (int)dr["BillCount"];
                    if (DBNull.Value.Equals(dr["PreviousOil"]))
                    {
                        car.PreviousOil = null;
                    }
                    else
                    {
                        car.PreviousOil = new int?((int)dr["PreviousOil"]);
                    }
                    if (DBNull.Value.Equals(dr["LastOil"]))
                    {
                        car.LastOil = null;
                    }
                    else
                    {
                        car.LastOil = new int?((int)dr["LastOil"]);
                    }
                    car.LastVolume = (double)dr["LastVolume"];
                    car.LastMileage = (double)dr["LastMileage"];
                    car.LastRate = (double)dr["LastRate"];
                    car.Description = (string)dr["Description"];
                    car.Creater = (int)dr["Creater"];
                    car.CreatedDate = (DateTime)dr["CreatedDate"];
                    car.Modifier = (int)dr["Modifier"];
                    car.ModifiedDate = (DateTime)dr["ModifiedDate"];
                    listCars.Add(car);
                }

                return listCars;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 获取单个车辆
        /// </summary>
        /// <param name="id"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static Model.Car GetCarById(int id, out string errMsg)
        {
            errMsg = "";
            try
            {
                string sql = "";
                SqlParameter[] para = new SqlParameter[]
				{
					new SqlParameter("@Id", id)
				};
                sql = "select * from [Car] where Id = @Id";
                DataTable dt = DBHelper.ExecuteGetDataTable(CommandType.Text, sql, para);
                Model.Car car = null;
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    car = new Car();
                    car.Id = (int)dr["Id"];
                    car.Number = (string)dr["Number"];
                    car.Vin = (string)dr["Vin"];
                    car.Model = (string)dr["Model"];
                    car.IsArchived = (bool)dr["IsArchived"];
                    car.InstanceCount = (int)dr["InstanceCount"];
                    car.BillCount = (int)dr["BillCount"];
                    if (DBNull.Value.Equals(dr["PreviousOil"]))
                    {
                        car.PreviousOil = null;
                    }
                    else
                    {
                        car.PreviousOil = new int?((int)dr["PreviousOil"]);
                    }
                    if (DBNull.Value.Equals(dr["LastOil"]))
                    {
                        car.LastOil = null;
                    }
                    else
                    {
                        car.LastOil = new int?((int)dr["LastOil"]);
                    }
                    car.LastVolume = (double)dr["LastVolume"];
                    car.LastMileage = (double)dr["LastMileage"];
                    car.LastRate = (double)dr["LastRate"];
                    car.Description = (string)dr["Description"];
                    car.Creater = (int)dr["Creater"];
                    car.CreatedDate = (DateTime)dr["CreatedDate"];
                    car.Modifier = (int)dr["Modifier"];
                    car.ModifiedDate = (DateTime)dr["ModifiedDate"];
                }
                else
                {
                    errMsg = "该车辆不存在或已被删除";
                }
                return car;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return null;
            }
        }
    }
}
