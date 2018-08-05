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
    /// 授权实体类
    /// </summary>
    [DataContract]
    public class Authorizer
    {
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public bool IsAuthorized { get; set; }
        [DataMember]
        public bool IsSystemer { get; set; }
        [DataMember]
        public bool IsManager { get; set; }
        [DataMember]
        public bool IsChecker { get; set; }
        [DataMember]
        public bool IsScanner { get; set; }
        [DataMember]
        public bool IsLoser { get; set; }
        [DataMember]
        public DateTime Sync { get; set; }
    }
}