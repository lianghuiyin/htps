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
    /// 角色实体类
    /// </summary>
    [DataContract]
    public class Role
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public IList<int> Powers { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int Creater { get; set; }
        [DataMember]
        public DateTime CreatedDate { get; set; }
        [DataMember]
        public int Modifier { get; set; }
        [DataMember]
        public DateTime ModifiedDate { get; set; }
    }

    [DataContract]
    public class RoleWrap
    {
        [DataMember]
        public Role Role { get; set; }
    }
}