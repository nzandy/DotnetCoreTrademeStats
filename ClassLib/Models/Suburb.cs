using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace DotnetCoreTrademeStats.ClassLib.Models {
	public class Suburb : TrademeListing {

		[Column("SuburbId")]
		[JsonProperty("SuburbId")]
		public override int Id { get; set; }
		public string Name { get; set; }
		[ForeignKey("DistrictId")]
		public virtual District District { get; set; }
	}
}