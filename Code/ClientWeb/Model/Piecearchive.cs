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
    /// 试件归档实体类
    /// </summary>
    [DataContract]
    public class Piecearchive
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int Piece { get; set; }
        [DataMember]
        public int Creater { get; set; }
        [DataMember]
        public DateTime CreatedDate { get; set; }
    }

    [DataContract]
    public class PiecearchiveWrap
    {
        [DataMember]
        public Piecearchive Piecearchive { get; set; }
    }
}