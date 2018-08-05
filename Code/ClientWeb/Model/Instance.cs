

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
    /// 申请单实体类
    /// </summary>
    [DataContract]
    public class Instance
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int Car { get; set; }
        [DataMember]
        public int Project { get; set; }
        [DataMember]
        public int Department { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public IList<int> Oils { get; set; }
        [DataMember]
        public string Goal { get; set; }
        [DataMember]
        public DateTime StartDate { get; set; }
        [DataMember]
        public DateTime EndDate { get; set; }
        [DataMember]
        public bool IsReleased { get; set; }
        [DataMember]
        public bool IsPending { get; set; }
        [DataMember]
        public bool IsArchived { get; set; }
        [DataMember]
        public bool IsEnable { get; set; }
        //[DataMember]
        //public IList<int> Traces { get; set; }
        //[DataMember]
        //public Nullable<int> Message { get; set; }
        [DataMember]
        public int BillCount { get; set; }
        [DataMember]
        public int Creater { get; set; }
        [DataMember]
        public DateTime CreatedDate { get; set; }
        [DataMember]
        public int Modifier { get; set; }
        [DataMember]
        public DateTime ModifiedDate { get; set; }
    }
}