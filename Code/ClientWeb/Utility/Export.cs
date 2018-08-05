namespace Utility
{
    using System;
    using System.Web;
    using System.Web.Security;
    using System.Data;
    using System.Text;

    public sealed class Export
    {
        /// <summary>
        /// ��������л��CSV��ʽ�ļ�
        /// </summary>
        /// <param name="dataTable">���ݱ�</param>
        /// <param name="fileName">����ļ���</param>
        public static void ResponseCSV(StringBuilder sb, string fileName)
        {
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            HttpContext.Current.Response.AppendHeader("Content-disposition", "attachment;filename=" + fileName);
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Write(sb.ToString());
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// ͨ��DataTable���CSV��ʽ����
        /// </summary>
        /// <param name="dataTable">���ݱ�</param>
        /// <returns>CSV�ַ�������</returns>
        public static StringBuilder GetCSVFormatData(DataTable dataTable,string sepChar)
        {
            StringBuilder StringBuilder = new StringBuilder();
            // д����ͷ
            foreach (DataColumn DataColumn in dataTable.Columns)
            {
                StringBuilder.Append(DataColumn.ColumnName.ToString() + sepChar);
            }
            StringBuilder.Append("\r\n");
            // д������
            foreach (DataRowView dataRowView in dataTable.DefaultView)
            {
                foreach (DataColumn DataColumn in dataTable.Columns)
                {
                    StringBuilder.AppendFormat("\"{0}\"{1}",dataRowView[DataColumn.ColumnName].ToString(),sepChar);
                }
                StringBuilder.Append("\r\n");
            }
            return StringBuilder;
        }
    }
}

