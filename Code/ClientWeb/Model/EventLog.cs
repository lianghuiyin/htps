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
    /// 事务日志实体类
    /// </summary>
    [DataContract]
    public class EventLog
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int User { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string TargetIds { get; set; }
        [DataMember]
        public string CodeTag { get; set; }
        [DataMember]
        public string LogName { get; set; }
        [DataMember]
        public string LogContent { get; set; }
        [DataMember]
        public string IpAddr { get; set; }
        [DataMember]
        public string UrlPath { get; set; }
        [DataMember]
        public bool IsSpecial { get; set; }
        [DataMember]
        public DateTime CreatedDate { get; set; }
    }
}