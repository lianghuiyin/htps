/***********************************************************************
 * Module:  Admin.cs
 * Author:  Administrator
 * Purpose: Definition of the Class Admin
 ***********************************************************************/

using System;
using System.Runtime.Serialization;

namespace Model
{
    /// <summary>
    /// 加油单报表实体类
    /// </summary>
    [DataContract]
    public class Report
    {
        [DataMember]
        public int Id
        {
            get;
            set;
        }

        [DataMember]
        public int Project
        {
            get;
            set;
        }

        [DataMember]
        public int Department
        {
            get;
            set;
        }

        [DataMember]
        public int Oil
        {
            get;
            set;
        }

        [DataMember]
        public double Volume
        {
            get;
            set;
        }

        [DataMember]
        public DateTime CreatedDate
        {
            get;
            set;
        }
    }

    [DataContract]
    public class ReportWrap
    {
        [DataMember]
        public Report Report
        {
            get;
            set;
        }
    }
}