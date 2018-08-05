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
    /// 权限实体类
    /// </summary>
    [DataContract]
    public class Power
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
    }


    public enum PowerStatusCode
    {
        None = 0,
        Systemer = 1,
        Manager = 2,
        Checker = 3,
        Scanner = 4,
        Loser = 5
    }
}