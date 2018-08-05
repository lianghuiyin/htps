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
    /// ϵͳ��վ�������ʵ����
    /// </summary>
    [DataContract]
    public class Changeset
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public DateTime SyncToken { get; set; }
        [DataMember]
        public IList<User> Users { get; set; }
        [DataMember]
        public IList<Role> Roles { get; set; }
        [DataMember]
        public IList<Project> Projects { get; set; }
        [DataMember]
        public IList<Department> Departments { get; set; }
        [DataMember]
        public IList<Oil> Oils { get; set; }
        [DataMember]
        public IList<Preference> Preferences { get; set; }
        [DataMember]
        public IList<Car> Cars { get; set; }
        [DataMember]
        public IList<Instance> Instances { get; set; }
        [DataMember]
        public IList<Trace> Traces { get; set; }
        [DataMember]
        public IList<Bill> Bills { get; set; }
        [DataMember]
        public IList<Piece> Pieces { get; set; }
        //[DataMember]
        //public IList<Signature> Signatures { get; set; }
        [DataMember]
        public IList<Deleted> Deleteds { get; set; }
    }
    [DataContract]
    public class ChangesetWrap
    {
        [DataMember]
        public Changeset Changeset { get; set; }
    }
}