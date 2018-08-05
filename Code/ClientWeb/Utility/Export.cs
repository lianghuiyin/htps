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
        /// 在浏览器中获得CSV格式文件
        /// </summary>
        /// <param name="dataTable">数据表</param>
        /// <param name="fileName">输出文件名</param>
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
        /// 通过DataTable获得CSV格式数据
        /// </summary>
        /// <param name="dataTable">数据表</param>
        /// <returns>CSV字符串数据</returns>
        public static StringBuilder GetCSVFormatData(DataTable dataTable,string sepChar)
        {
            StringBuilder StringBuilder = new StringBuilder();
            // 写出表头
            foreach (DataColumn DataColumn in dataTable.Columns)
            {
                StringBuilder.Append(DataColumn.ColumnName.ToString() + sepChar);
            }
            StringBuilder.Append("\r\n");
            // 写出数据
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

