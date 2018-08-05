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
    /// 项目逻辑类
    /// </summary>
    public static class PieceManager
    {
        /// <summary>
        /// 添加项目
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddPiece(ref Model.Piece model, out string errMsg)
        {
            return PieceService.AddPiece(ref model, out errMsg);
        }

        /// <summary>
        /// 根据id修改项目
        /// </summary>
        /// <param name="Model.Piece"></param>
        /// <returns></returns>
        public static bool ModifyPieceById(ref Model.Piece model, out string errMsg)
        {
            return PieceService.ModifyPieceById(ref model, out errMsg);
        }

        /// <summary>
        /// 删除项目
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
    }
}
