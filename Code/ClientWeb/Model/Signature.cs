using System;
using System.Runtime.Serialization;

namespace Model
{
	[DataContract]
	public class Signature
	{
		[DataMember]
		public int Id
		{
			get;
			set;
		}

		[DataMember]
		public string Name
		{
			get;
			set;
		}

		[DataMember]
		public string Sign
		{
			get;
			set;
		}

		[DataMember]
		public int Creater
		{
			get;
			set;
		}

		[DataMember]
		public DateTime CreatedDate
		{
			get;
			set;
		}
	}

    [DataContract]
    public class SignatureWrap
    {
        [DataMember]
        public Signature Signature
        {
            get;
            set;
        }
    }
}
