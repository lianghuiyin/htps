namespace Utility
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class Pager
    {
        private string tblName = "";
        private string strGetFields = "*";
        private string orderField = "";
        private int orderType = 0;
        private string strWhere = "";
        private int pageSize = 10;
        private int pageIndex = 1;

        public string TblName { get{ return this.tblName;} set{ this.tblName = value;}}
        public string StrGetFields { get { return this.strGetFields; } set { this.strGetFields = value; } }
        public string OrderField { get { return this.orderField; } set { this.orderField = value; } }
        public int OrderType { get { return this.orderType; } set { this.orderType = value; } }
        public string StrWhere { get { return this.strWhere; } set { this.strWhere = value; } }
        public int PageSize { get { return this.pageSize; } set { this.pageSize = value; } }
        public int PageIndex { get { return this.pageIndex; } set { this.pageIndex = value; } }

        public DataTable ExecuteDataTable()
        {
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@TblName", this.tblName),
                new SqlParameter("@StrGetFields", this.strGetFields),
                new SqlParameter("@OrderField", this.orderField),
                new SqlParameter("@OrderType", this.orderType),
                new SqlParameter("@StrWhere", this.strWhere),
                new SqlParameter("@PageSize", this.pageSize),
                new SqlParameter("@PageIndex", this.pageIndex),
                new SqlParameter("@DoCount",0)
            };
            DataTable reData = DBHelper.ExecuteGetDataTable(CommandType.StoredProcedure, "proc_Pager", para);
            return reData;
        }

        public int ExecuteCount()
        {
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@TblName", this.tblName),
                new SqlParameter("@StrGetFields", this.strGetFields),
                new SqlParameter("@OrderField", this.orderField),
                new SqlParameter("@OrderType", this.orderType),
                new SqlParameter("@StrWhere", this.strWhere),
                new SqlParameter("@PageSize", this.pageSize),
                new SqlParameter("@PageIndex", this.pageIndex),
                new SqlParameter("@DoCount",1)
            };
            int reCount = (int)DBHelper.ExecuteScalar(CommandType.StoredProcedure, "proc_Pager", para);
            return reCount;
        }

        public string ExecuteCountSqlStr()
        {
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@TblName", this.tblName),
                new SqlParameter("@StrGetFields", this.strGetFields),
                new SqlParameter("@OrderField", this.orderField),
                new SqlParameter("@OrderType", this.orderType),
                new SqlParameter("@StrWhere", this.strWhere),
                new SqlParameter("@PageSize", this.pageSize),
                new SqlParameter("@PageIndex", this.pageIndex),
                new SqlParameter("@DoCount",1),
                new SqlParameter("@DoSqlStr",1)
            };
            return (string)DBHelper.ExecuteScalar(CommandType.StoredProcedure, "proc_Pager", para);
        }

        public string ExecuteDataTableSqlStr()
        {
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@TblName", this.tblName),
                new SqlParameter("@StrGetFields", this.strGetFields),
                new SqlParameter("@OrderField", this.orderField),
                new SqlParameter("@OrderType", this.orderType),
                new SqlParameter("@StrWhere", this.strWhere),
                new SqlParameter("@PageSize", this.pageSize),
                new SqlParameter("@PageIndex", this.pageIndex),
                new SqlParameter("@DoCount",0),
                new SqlParameter("@DoSqlStr",1)
            };
            return (string)DBHelper.ExecuteScalar(CommandType.StoredProcedure, "proc_Pager", para);
        }
    }
}

