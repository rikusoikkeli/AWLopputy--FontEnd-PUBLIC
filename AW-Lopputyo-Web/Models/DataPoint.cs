using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace loppuprojekti.Models
{
    [DataContract]
    public class DataPoint
    {
		public DataPoint(string label, double? y)
		{
			this.Label = label;
			this.Y = y;
		}

		[DataMember(Name = "label")]
		public string Label = "";

		[DataMember(Name = "y")]
		public double? Y = null;
	}
}
