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
    /// 申请单流转历程实体类
    /// </summary>
    [DataContract]
    public class Trace
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int Car { get; set; }
        [DataMember]
        public int Instance { get; set; }
        [DataMember]
        public Nullable<int> PreviousTrace { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public bool IsFinished { get; set; }
        [DataMember]
        public bool IsArchived { get; set; }
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
        public string StartInfo { get; set; }
        [DataMember]
        public string EndInfo { get; set; }
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