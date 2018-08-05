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
    /// 申请单归档实体类
    /// </summary>
    [DataContract]
    public class Instancearchive
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int Instance { get; set; }
        [DataMember]
        public int Car { get; set; }
        [DataMember]
        public int Creater { get; set; }
        [DataMember]
        public DateTime CreatedDate { get; set; }
    }

    [DataContract]
    public class InstancearchiveWrap
    {
        [DataMember]
        public Instancearchive Instancearchive { get; set; }
    }
}