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
    /// 车辆归档实体类
    /// </summary>
    [DataContract]
    public class Cararchive
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int Car { get; set; }
        [DataMember]
        public int Creater { get; set; }
        [DataMember]
        public DateTime CreatedDate { get; set; }
    }

    [DataContract]
    public class CararchiveWrap
    {
        [DataMember]
        public Cararchive Cararchive { get; set; }
    }
}