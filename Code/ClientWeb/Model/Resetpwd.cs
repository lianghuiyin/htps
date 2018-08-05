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
    /// 密码重置实体类
    /// </summary>
    [DataContract]
    public class Resetpwd
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int User { get; set; }
        [DataMember]
        public string NewPassword { get; set; }
    }

    [DataContract]
    public class ResetpwdWrap
    {
        [DataMember]
        public Resetpwd Resetpwd { get; set; }
    }
}