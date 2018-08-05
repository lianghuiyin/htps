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
    /// 偏好设置实体类
    /// </summary>
    [DataContract]
    public class Preference
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int ShortcutHour { get; set; }
        [DataMember]
        public int FinishHour { get; set; }
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
    public class PreferenceWrap
    {
        [DataMember]
        public Preference Preference { get; set; }
    }
}