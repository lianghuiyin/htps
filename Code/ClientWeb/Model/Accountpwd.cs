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
    /// 密码修改实体类
    /// </summary>
    [DataContract]
    public class Accountpwd
    {
        [DataMember]
        public int User { get; set; }
        [DataMember]
        public string OldPassword { get; set; }
        [DataMember]
        public string NewPassword { get; set; }
        [DataMember]
        public string ConfirmPassword { get; set; }
    }

    [DataContract]
    public class AccountpwdWrap
    {
        [DataMember]
        public Accountpwd Accountpwd { get; set; }
    }
}