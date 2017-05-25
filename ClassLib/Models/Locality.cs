using System;
using System.Collections.Generic;
using DotnetCoreTrademeStats.ClassLib.Attributes;
using Newtonsoft.Json;

namespace DotnetCoreTrademeStats.ClassLib.Models {

	[TrademeListing("v1/Localities.json?")]
	public class Locality : TrademeListing {

		[JsonProperty("LocalityId")]
		public override int Id {get; set;}
		public string Name {get; set;}
		public List<District> Districts {get; set;}
	}
}