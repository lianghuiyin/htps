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
    /// 新建申请单实体类
    /// </summary>
    [DataContract]
    public class Instancenew
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
        public string StartInfo { get; set; }
        [DataMember]
        public int Creater { get; set; }
        [DataMember]
        public DateTime CreatedDate { get; set; }
    }

    [DataContract]
    public class InstancenewWrap
    {
        [DataMember]
        public Instancenew Instancenew { get; set; }
    }
}