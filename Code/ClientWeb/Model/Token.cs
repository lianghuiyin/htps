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
    /// —È÷§¿‡
    /// </summary>
    [DataContract]
    public class Token
    {
        [DataMember]
        public int Exp { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Powers { get; set; }
        [DataMember]
        public DateTime Sync { get; set; }
    }
}