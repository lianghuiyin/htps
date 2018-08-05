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
    /// 申请单报表搜索条件实体类
    /// </summary>
    [DataContract]
    public class ReportsFilter
    {
        [DataMember]
        public int? Project { get; set; }
        [DataMember]
        public int? Department { get; set; }
        [DataMember]
        public DateTime StartDate { get; set; }
        [DataMember]
        public DateTime EndDate { get; set; }
    }
}