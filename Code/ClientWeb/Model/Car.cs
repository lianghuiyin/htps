/***********************************************************************
 * Module:  Admin.cs
 * Author:  Administrator
 * Purpose: Definition of the Class Admin
 ***********************************************************************/

using System;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Model
{
    /// <summary>
    /// 车辆实体类
    /// </summary>
    [DataContract]
    public class Car
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Number { get; set; }
        [DataMember]
        public string Vin { get; set; }
        [DataMember]
        public string Model { get; set; }
        [DataMember]
        public bool IsArchived { get; set; }
        //[DataMember]
        //public IList<int> Instances { get; set; }
        [DataMember]
        public int InstanceCount { get; set; }
        [DataMember]
        public int BillCount { get; set; }
        [DataMember]
        public Nullable<int> PreviousOil { get; set; }
        [DataMember]
        public Nullable<int> LastOil { get; set; }
        [DataMember]
        public double LastVolume { get; set; }
        [DataMember]
        public double LastMileage { get; set; }
        [DataMember]
        public double LastRate { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int Creater { get; set; }
        [DataMember]
        public DateTime CreatedDate { get; set; }
        [DataMember]
        public int Modifier { get; set; }
        [DataMember]
        public DateTime ModifiedDate { get; set; }
    }

    [DataContract]
    public class CarWrap
    {
        [DataMember]
        public Car Car { get; set; }
    }
}