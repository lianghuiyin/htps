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
    /// 试件逻辑类
    /// </summary>
    public static class PieceManager
    {
        /// <summary>
        /// 添加试件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddPiece(ref Model.Piece model, out string errMsg)
        {
            return PieceService.AddPiece(ref model, out errMsg);
        }

        /// <summary>
        /// 根据id修改试件
        /// </summary>
        /// <param name="Model.Piece"></param>
        /// <returns></returns>
        public static bool ModifyPieceById(ref Model.Piece model, out string errMsg)
        {
            return PieceService.ModifyPieceById(ref model, out errMsg);
        }

        /// <summary>
        /// 删除试件
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static bool DeletePieceByIds(string ids, out string errMsg)
        {
            return PieceService.DeletePieceByIds(ids, out errMsg);
        }

        /// <summary>
        /// 检查是否重复
        /// </summary>
        /// <param name="Model.Piece"></param>
        /// <returns></returns>
        public static bool CheckRepeat(Model.Piece model)
        {
            return PieceService.CheckRepeat(model);
        }

        /// <summary>
        /// 根据id打印试件
        /// </summary>
        /// <param name="Model.Piece"></param>
        /// <returns></returns>
        public static bool PrintPieceById(ref Model.PiecePrinter model, out string errMsg)
        {
            return PieceService.PrintPieceById(ref model, out errMsg);
        }

        /// <summary>
        /// 试件归档
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool ArchivePiece(ref Model.Piecearchive model, out string errMsg)
        {
            return PieceService.ArchivePiece(ref model, out errMsg);
        }

        /// <summary>
        /// 试件还原
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool RestorePiece(ref Model.Piecerestore model, out string errMsg)
        {
            return PieceService.RestorePiece(ref model, out errMsg);
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
            return PieceService.GetArchivedPieces(key, count, lastId, out errMsg);
        }
    }
}
