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
    /// 项目实体类
    /// </summary>
    [DataContract]
    public class Project
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public bool IsEnable { get; set; }
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
    public class ProjectWrap
    {
        [DataMember]
        public Project Project { get; set; }
    }
}