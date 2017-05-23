using System;
using System.Collections.Generic;

namespace DotnetCoreTrademeStats.ClassLib.Models {

	public class District {
		public string Name {get; set;}
		public List<Suburb> Suburbs {get; set;}
	}
}