using System;
using System.Collections.Generic;
using DotnetCoreTrademeStats.ClassLib.Attributes;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
namespace DotnetCoreTrademeStats.ClassLib.Models {

	[TrademeListing("v1/Localities.json?")]
	public class Locality : TrademeListing {

		[JsonProperty("LocalityId")]
		[Column("LocalityId")]
		public override int Id {get; set;}
		public string Name {get; set;}
		public ICollection<District> Districts {get; set;}
	}
}