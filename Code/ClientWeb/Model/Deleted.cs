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
    /// Changeset中变更数据的实体类
    /// </summary>
    [DataContract]
    public class Deleted
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Model { get; set; }
        [DataMember]
        public string TargetIds { get; set; }
        [DataMember]
        public DateTime CreatedDate { get; set; }
    }
}