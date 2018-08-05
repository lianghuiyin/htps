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
    /// 部门实体类
    /// </summary>
    [DataContract]
    public class Department
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
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
    public class DepartmentWrap
    {
        [DataMember]
        public Department Department { get; set; }
    }
}