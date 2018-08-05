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
    /// 申请单中止实体类
    /// </summary>
    [DataContract]
    public class Instanceabort
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int Trace { get; set; }
        [DataMember]
        public int Instance { get; set; }
        [DataMember]
        public int Car { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string StartInfo { get; set; }
        [DataMember]
        public int Creater { get; set; }
        [DataMember]
        public DateTime CreatedDate { get; set; }
    }

    [DataContract]
    public class InstanceabortWrap
    {
        [DataMember]
        public Instanceabort Instanceabort { get; set; }
    }
}