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
    /// 试件数据访问类
    /// </summary>
    public static class PieceService
    {
        /// <summary>
        /// 添加试件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddPiece(ref Model.Piece model, out string errMsg)
        {
            errMsg = "";
            try
            {
                model.Name = model.Name.Trim();
                model.Description = model.Description.Trim();
                SqlParameter[] para = new SqlParameter[] 
                {
                    new SqlParameter("@Name", model.Name),
                    new SqlParameter("@Number", model.Number),
                    new SqlParameter("@Order", model.Order),
                    new SqlParameter("@Count", model.Count),
                    new SqlParameter("@PrintedCount", model.PrintedCount),
                    new SqlParameter("@IsPrinted", model.IsPrinted),
                    new SqlParameter("@Ots", model.Ots),
                    new SqlParameter("@DelegateNumber", model.DelegateNumber),
                    new SqlParameter("@AccessoryFactory", model.AccessoryFactory),
                    new SqlParameter("@VehicleType", model.VehicleType),
                    new SqlParameter("@TestContent", model.TestContent),
                    new SqlParameter("@SendPerson", model.SendPerson),
                    new SqlParameter("@ChargePerson", model.ChargePerson),
                    new SqlParameter("@SendDate", model.SendDate),
                    new SqlParameter("@Place", model.Place),
                    new SqlParameter("@IsEnable", model.IsEnable),
                    new SqlParameter("@Description", model.Description),
                    new SqlParameter("@Creater", model.Creater),
                    new SqlParameter("@CreatedDate", model.CreatedDate),
                    new SqlParameter("@Modifier", model.Modifier),
                    new SqlParameter("@ModifiedDate", model.ModifiedDate),
                    new SqlParameter("@return",SqlDbType.Int)
                };
                para[21].Direction = ParameterDirection.ReturnValue;
                int i = DBHelper.ExecuteNonQuery(CommandType.StoredProcedure, "proc_PieceInsert", para);
                if (i > 0)
                {
                    model.Id = int.Parse(para[21].Value.ToString());
                    EventLog e = new EventLog();
                    e.TargetIds = para[21].Value.ToString();
                    e.CodeTag = "AddPiece";
                    e.LogName = "添加试件";
                    EventLogService.AddEventLog<Model.Piece>(e, model);
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
                e.CodeTag = "AddPiece";
                e.LogName = "添加试件";
                e.ErrorMsg = ex.Message.ToString();
                ErrorLogService.AddErrorLog<Model.Piece>(e, model);
                return false;
            }
        }

        /// <summary>
        /// 根据id修改试件
        /// </summary>
        /// <param name="Model.Piece"></param>
        /// <returns></returns>
        public static bool ModifyPieceById(ref Model.Piece model, out string errMsg)
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
                    new SqlParameter("@Number", model.Number),
                    new SqlParameter("@Order", model.Order),
                    new SqlParameter("@Count", model.Count),
                    new SqlParameter("@PrintedCount", model.PrintedCount),
                    new SqlParameter("@IsPrinted", model.IsPrinted),
                    new SqlParameter("@Ots", model.Ots),
                    new SqlParameter("@DelegateNumber", model.DelegateNumber),
                    new SqlParameter("@AccessoryFactory", model.AccessoryFactory),
                    new SqlParameter("@VehicleType", model.VehicleType),
                    new SqlParameter("@TestContent", model.TestContent),
                    new SqlParameter("@SendPerson", model.SendPerson),
                    new SqlParameter("@ChargePerson", model.ChargePerson),
                    new SqlParameter("@SendDate", model.SendDate),
                    new SqlParameter("@Place", model.Place),
                    new SqlParameter("@IsEnable", model.IsEnable),
                    new SqlParameter("@Description", model.Description),
                    new SqlParameter("@Modifier", model.Modifier),
                    new SqlParameter("@ModifiedDate", model.ModifiedDate)
                };
                int i = DBHelper.ExecuteNonQuery(CommandType.StoredProcedure, "proc_PieceByIdUpdate", para);
                if (i > 0)
                {
                    EventLog e = new EventLog();
                    e.TargetIds = model.Id.ToString();
                    e.CodeTag = "ModifyPieceById";
                    e.LogName = "修改试件";
                    EventLogService.AddEventLog<Model.Piece>(e, model);
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
                e.CodeTag = "ModifyPieceById";
                e.LogName = "修改试件";
                e.ErrorMsg = ex.Message.ToString();
                ErrorLogService.AddErrorLog<Model.Piece>(e, model);
                return false;
            }
        }

        /// <summary>
        /// 删除试件
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static bool DeletePieceByIds(string ids, out string errMsg)
        {
            errMsg = "";
            try
            {
                SqlParameter[] para = new SqlParameter[] 
                {
                    new SqlParameter("@model", "Piece"),
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
                        e.CodeTag = "DeletePieceByIds";
                        e.LogName = "删除试件";
                        EventLogService.AddEventLog<Model.Piece>(e, null);
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
                e.CodeTag = "DeletePieceByIds";
                e.LogName = "删除试件";
                e.ErrorMsg = ex.Message.ToString();
                ErrorLogService.AddErrorLog<Model.Piece>(e,null);
                return false;
            }
        }

        /// <summary>
        /// 检查是否重复
        /// </summary>
        /// <param name="Model.Piece"></param>
        /// <returns></returns>
        public static bool CheckRepeat(Model.Piece model)
        {
            try
            {
                string sql = "select * from Piece where Name = @Name and Id <> @Id";
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
                e.LogName = "检查是否重复-ForPiece";
                e.ErrorMsg = ex.Message.ToString();
                ErrorLogService.AddErrorLog<Model.Piece>(e, model);
                return true;
            }
        }

        /// <summary>
        /// 根据id打印试件
        /// </summary>
        /// <param name="Model.Piece"></param>
        /// <returns></returns>
        public static bool PrintPieceById(ref Model.PiecePrinter model, out string errMsg)
        {
            errMsg = "";
            try
            {
                SqlParameter[] para = new SqlParameter[] 
                {
                    new SqlParameter("@Id", model.Id),
                    new SqlParameter("@Count", model.Count),
                    new SqlParameter("@Modifier", model.Modifier),
                    new SqlParameter("@ModifiedDate", model.ModifiedDate)
                };
                int i = DBHelper.ExecuteNonQuery(CommandType.StoredProcedure, "proc_PiecePrint", para);
                if (i > 0)
                {
                    EventLog e = new EventLog();
                    e.TargetIds = model.Id.ToString();
                    e.CodeTag = "PrintPieceById";
                    e.LogName = "打印试件";
                    EventLogService.AddEventLog<Model.PiecePrinter>(e, model);
                    return true;
                }
                else
                {
                    errMsg = "该试件已被删除，不能打印！";
                    return false;
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                ErrorLog e = new ErrorLog();
                e.TargetIds = model.Id.ToString();
                e.CodeTag = "ModifyPieceById";
                e.LogName = "打印试件";
                e.ErrorMsg = ex.Message.ToString();
                ErrorLogService.AddErrorLog<Model.PiecePrinter>(e, model);
                return false;
            }
        }

        /// <summary>
        /// 获取已归档试件
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <param name="lastId"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static IList<Model.Piece> GetArchivedPieces(string key, int count, int lastId, out string errMsg)
        {
            errMsg = "";
            try
            {
                if (key == null)
                {
                    key = "";
                }
                else
                {
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
                    keyWhere = " (Name like @Key or Number like @Key or AccessoryFactory like @Key or VehicleType like @Key or Place like @Key) and ";
                }
                string lastIdWhere = "";
                if (lastId > 0)
                {
                    lastIdWhere = " and Id < @LastId ";
                }
                string sql = string.Format("select top (@Count) * from [Piece] where {0} IsArchived = 1 {1} order by Id desc", keyWhere, lastIdWhere);
                DataTable dt = DBHelper.ExecuteGetDataTable(CommandType.Text, sql, para);

                IList<Model.Piece> listPieces = new List<Model.Piece>();
                foreach (DataRow dr in dt.Rows)
                {
                    Piece piece = new Piece();
                    piece.Id = (int)dr["Id"];
                    piece.Name = (string)dr["Name"];
                    piece.Number = (string)dr["Number"];
                    piece.Order = (string)dr["Order"];
                    piece.Count = (int)dr["Count"];
                    piece.PrintedCount = (int)dr["PrintedCount"];
                    piece.IsPrinted = (bool)dr["IsPrinted"];
                    piece.Ots = (string)dr["Ots"];
                    piece.DelegateNumber = (string)dr["DelegateNumber"];
                    piece.AccessoryFactory = (string)dr["AccessoryFactory"];
                    piece.VehicleType = (string)dr["VehicleType"];
                    piece.TestContent = (string)dr["TestContent"];
                    piece.SendPerson = (string)dr["SendPerson"];
                    piece.ChargePerson = (string)dr["ChargePerson"];
                    if (DBNull.Value.Equals(dr["SendDate"]))
                    {
                        piece.SendDate = null;
                    }
                    else
                    {
                        piece.SendDate = (DateTime?)dr["SendDate"];
                    }
                    piece.Place = (string)dr["Place"];
                    piece.IsEnable = (bool)dr["IsEnable"];
                    piece.IsArchived = (bool)dr["IsArchived"];
                    piece.Description = (string)dr["Description"];
                    piece.Creater = (int)dr["Creater"];
                    piece.CreatedDate = (DateTime)dr["CreatedDate"];
                    piece.Modifier = (int)dr["Modifier"];
                    piece.ModifiedDate = (DateTime)dr["ModifiedDate"];
                    listPieces.Add(piece);
                }

                return listPieces;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return null;
            }
        }
    }
}
