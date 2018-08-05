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
    /// µÇÂ¼ÊµÌåÀà
    /// </summary>
    [DataContract]
    public class Login
    {
        [DataMember]
        public Nullable<int> LogId { get; set; }
        [DataMember]
        public string LogName { get; set; }
        [DataMember]
        public string LogPassword { get; set; }
        [DataMember]
        public bool IsPassed { get; set; }
    }

    [DataContract]
    public class LoginWrap
    {
        [DataMember]
        public Login Login { get; set; }
    }
}