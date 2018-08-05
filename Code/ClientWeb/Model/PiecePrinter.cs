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
    /// 试件打印实体类
    /// </summary>
    [DataContract]
    public class PiecePrinter
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int Count { get; set; }
        [DataMember]
        public string Content { get; set; }
        [DataMember]
        public int Modifier { get; set; }
        [DataMember]
        public DateTime ModifiedDate { get; set; }
    }

    [DataContract]
    public class PiecePrinterWrap
    {
        [DataMember]
        public PiecePrinter PiecePrinter { get; set; }
    }
}