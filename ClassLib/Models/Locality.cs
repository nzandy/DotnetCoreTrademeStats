using System;
using System.Collections.Generic;
using DotnetCoreTrademeStats.ClassLib.Attributes;

namespace DotnetCoreTrademeStats.ClassLib.Models {

	[TrademeListing("v1/Localities.json?")]
	public class Locality {
		public string Name {get; set;}
		public List<District> Districts {get; set;}
	}
}