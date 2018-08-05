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
    /// 一键优化实体类
    /// </summary>
    [DataContract]
    public class Optimizer
    {
        [DataMember]
        public int Id
        {
            get;
            set;
        }

        [DataMember]
        public int AllCarsCount
        {
            get;
            set;
        }

        [DataMember]
        public int ArchivedCarsCount
        {
            get;
            set;
        }

        [DataMember]
        public int ArchivedInstancesCount
        {
            get;
            set;
        }
        [DataMember]
        public int Creater { get; set; }
        [DataMember]
        public DateTime CreatedDate { get; set; }
    }

    [DataContract]
    public class OptimizerWrap
    {
        [DataMember]
        public Optimizer Optimizer
        {
            get;
            set;
        }
    }
}