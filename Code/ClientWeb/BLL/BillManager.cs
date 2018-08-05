using DAL;
using Model;
using System;
using System.Collections.Generic;

namespace BLL
{
	public static class BillManager
	{
		public static bool AddBill(ref Bill model, out string errMsg)
		{
			return BillService.AddBill(ref model, out errMsg);
		}

        /// <summary>
        /// 根据id修改加油单
        /// </summary>
        /// <param name="Model.Bill"></param>
        /// <returns></returns>
        public static bool ModifyBillById(ref Model.Bill model, out string errMsg)
        {
            return BillService.ModifyBillById(ref model, out errMsg);
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
            return BillService.GetBills(count,billsFilter, out errMsg);
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
            return BillService.GetBillsByTime(startDate, endDate, out errMsg);
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
            return BillService.GetLastOneBillByCar(id, car, out errMsg);
        }

        /// <summary>
        /// 获取单个加油单
        /// </summary>
        /// <param name="id"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static Model.Bill GetBillById(int id, out string errMsg)
        {
            return BillService.GetBillById(id, out errMsg);
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
            return BillService.GetReports(reportsFilter, out errMsg);
        }

        /// <summary>
        /// 获取加油单小票打印内容
        /// </summary>
        /// <param name="lastId"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static List<Model.BillPrinter> GetBillsForPrinter(int lastId, out string errMsg)
        {
            return BillService.GetBillsForPrinter(lastId,out errMsg);
        }

        /// <summary>
        /// 修改加油单打印状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static bool ModifyBillPrintedById(int id, out string errMsg)
        {
            return BillService.ModifyBillPrintedById(id, out errMsg);
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
            return BillService.GetBillsForExport(count, billsFilter, out errMsg);
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
            return BillService.GetBillsCountForExport(billsFilter, out errMsg);
        }
	}
}
