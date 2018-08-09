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
    /// 试件还原实体类
    /// </summary>
    [DataContract]
    public class Piecerestore
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
    public class PiecerestoreWrap
    {
        [DataMember]
        public Piecerestore Piecerestore { get; set; }
    }
}