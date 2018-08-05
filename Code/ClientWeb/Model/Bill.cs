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
    /// 加油单实体类
    /// </summary>
    [DataContract]
    public class Bill
    {
        [DataMember]
        public int Id
        {
            get;
            set;
        }

        [DataMember]
        public int Car
        {
            get;
            set;
        }

        [DataMember]
        public int Instance
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
        public int? PreviousOil
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
        public int? Signature
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
        public int Oiler
        {
            get;
            set;
        }

        [DataMember]
        public DateTime Time
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

        [DataMember]
        public bool IsPrinted
        {
            get;
            set;
        }

        [DataMember]
        public int Creater
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

        [DataMember]
        public int Modifier
        {
            get;
            set;
        }

        [DataMember]
        public DateTime ModifiedDate
        {
            get;
            set;
        }
    }

    [DataContract]
    public class BillWrap
    {
        [DataMember]
        public Bill Bill
        {
            get;
            set;
        }
    }
}