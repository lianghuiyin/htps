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
    /// 加油单打印内容实体类
    /// </summary>
    [DataContract]
    public class BillPrinter
    {
        [DataMember]
        public int Id
        {
            get;
            set;
        }

        [DataMember]
        public string CarNumber
        {
            get;
            set;
        }

        [DataMember]
        public string CarVin
        {
            get;
            set;
        }

        [DataMember]
        public string ProjectName
        {
            get;
            set;
        }

        [DataMember]
        public string DepartmentName
        {
            get;
            set;
        }

        [DataMember]
        public string OilName
        {
            get;
            set;
        }

        [DataMember]
        public string UserName
        {
            get;
            set;
        }

        [DataMember]
        public string ApplicantName
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
        public double Mileage
        {
            get;
            set;
        }

        [DataMember]
        public string DriverName
        {
            get;
            set;
        }

        [DataMember]
        public double Rate
        {
            get;
            set;
        }

        [DataMember]
        public string OilerName
        {
            get;
            set;
        }

        //[DataMember]
        //public string CreaterName
        //{
        //    get;
        //    set;
        //}

        [DataMember]
        public DateTime Time
        {
            get;
            set;
        }

        //[DataMember]
        //public DateTime CreatedDate
        //{
        //    get;
        //    set;
        //}

        [DataMember]
        public bool IsPrinted
        {
            get;
            set;
        }

        [DataMember]
        public bool IsLost
        {
            get;
            set;
        }
    }
}