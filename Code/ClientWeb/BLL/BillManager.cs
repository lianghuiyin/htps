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
        /// ����id�޸ļ��͵�
        /// </summary>
        /// <param name="Model.Bill"></param>
        /// <returns></returns>
        public static bool ModifyBillById(ref Model.Bill model, out string errMsg)
        {
            return BillService.ModifyBillById(ref model, out errMsg);
        }

        /// <summary>
        /// ��ȡ���͵��б�
        /// </summary>
        /// <param name="count">Ҫ���صļ�¼����</param>
        /// <param name="billsFilter">ɸѡ����</param>
        /// <param name="lastId">���С��idֵ</param>
        /// <param name="errMsg">������Ϣ</param>
        /// <returns></returns>
        public static IList<Model.Bill> GetBills(
            int? count,
            BillsFilter billsFilter,
            out string errMsg)
        {
            return BillService.GetBills(count,billsFilter, out errMsg);
        }

        /// <summary>
        /// ����ʱ���ȡ���͵��б�
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
        /// ��ȡָ�����뵥������һ�μ��͵�
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
        /// ��ȡ�������͵�
        /// </summary>
        /// <param name="id"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static Model.Bill GetBillById(int id, out string errMsg)
        {
            return BillService.GetBillById(id, out errMsg);
        }
        
        /// <summary>
        /// ��ȡ���͵�����
        /// </summary>
        /// <param name="billsFilter">ɸѡ����</param>
        /// <param name="errMsg">������Ϣ</param>
        /// <returns></returns>
        public static IList<Model.Report> GetReports(
            ReportsFilter reportsFilter,
            out string errMsg)
        {
            return BillService.GetReports(reportsFilter, out errMsg);
        }

        /// <summary>
        /// ��ȡ���͵�СƱ��ӡ����
        /// </summary>
        /// <param name="lastId"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static List<Model.BillPrinter> GetBillsForPrinter(int lastId, out string errMsg)
        {
            return BillService.GetBillsForPrinter(lastId,out errMsg);
        }

        /// <summary>
        /// �޸ļ��͵���ӡ״̬
        /// </summary>
        /// <param name="id"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static bool ModifyBillPrintedById(int id, out string errMsg)
        {
            return BillService.ModifyBillPrintedById(id, out errMsg);
        }

        /// <summary>
        /// ��ȡ���͵������б�
        /// </summary>
        /// <param name="count">Ҫ���صļ�¼����</param>
        /// <param name="billsFilter">ɸѡ����</param>
        /// <param name="lastId">���С��idֵ</param>
        /// <param name="errMsg">������Ϣ</param>
        /// <returns></returns>
        public static IList<Model.BillPrinter> GetBillsForExport(
            int? count,
            BillsFilter billsFilter,
            out string errMsg)
        {
            return BillService.GetBillsForExport(count, billsFilter, out errMsg);
        }
        
        /// <summary>
        /// ��ȡ���͵������б���͵�����
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
