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
    /// 试件实体类
    /// </summary>
    [DataContract]
    public class Piece
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Number { get; set; }
        [DataMember]
        public string Order { get; set; }
        [DataMember]
        public int Count { get; set; }
        [DataMember]
        public int PrintedCount { get; set; }
        [DataMember]
        public bool IsPrinted { get; set; }
        [DataMember]
        public string Ots { get; set; }
        [DataMember]
        public string DelegateNumber { get; set; }
        [DataMember]
        public string AccessoryFactory { get; set; }
        [DataMember]
        public string VehicleType { get; set; }
        [DataMember]
        public string TestContent { get; set; }
        [DataMember]
        public string SendPerson { get; set; }
        [DataMember]
        public string ChargePerson { get; set; }
        [DataMember]
        public DateTime? SendDate { get; set; }
        [DataMember]
        public string Place { get; set; }
        [DataMember]
        public bool IsEnable { get; set; }
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
    public class PieceWrap
    {
        [DataMember]
        public Piece Piece { get; set; }
    }
}