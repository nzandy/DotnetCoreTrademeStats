using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace DotnetCoreTrademeStats.ClassLib.Models {

	public class District : TrademeListing{
		[Column("DistrictId")]
		[JsonProperty("DistrictId")]
		public override int Id { get; set; }
		public string Name { get; set; }
		public ICollection<Suburb> Suburbs { get; set ;}

		[ForeignKey("LocalityId")]
		public virtual Locality Locality { get; set; }
	}
}