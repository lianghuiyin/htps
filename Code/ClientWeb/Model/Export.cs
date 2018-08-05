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
    /// ����EXCELʵ����
    /// </summary>
    [DataContract]
    public class Export
    {
        [DataMember]
        public int Id
        {
            get;
            set;
        }

        [DataMember]
        public int Length
        {
            get;
            set;
        }

        [DataMember]
        public int Total
        {
            get;
            set;
        }

        [DataMember]
        public int LastId
        {
            get;
            set;
        }
    }
}