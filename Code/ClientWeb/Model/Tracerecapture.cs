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
    /// 取回申请单实体类
    /// </summary>
    [DataContract]
    public class Tracerecapture
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
        public string EndInfo { get; set; }
        [DataMember]
        public int Creater { get; set; }
        [DataMember]
        public DateTime CreatedDate { get; set; }
    }

    [DataContract]
    public class TracerecaptureWrap
    {
        [DataMember]
        public Tracerecapture Tracerecapture { get; set; }
    }
}