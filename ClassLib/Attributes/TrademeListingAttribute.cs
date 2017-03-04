using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetCoreTrademeStats.ClassLib.Attributes {
	public class TrademeListingAttribute : Attribute {
		public TrademeListingAttribute(string apiPath) {
			ApiPath = apiPath;
		}

		public string ApiPath { get; private set; }
	}
}
