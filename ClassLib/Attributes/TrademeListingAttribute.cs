using System;

namespace DotnetCoreTrademeStats.ClassLib.Attributes {
	public class TrademeListingAttribute : Attribute {
		public TrademeListingAttribute(string apiPath) {
			ApiPath = apiPath;
		}

		public string ApiPath { get; private set; }
	}
}
