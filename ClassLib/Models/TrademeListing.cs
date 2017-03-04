using System.ComponentModel.DataAnnotations;

namespace DotnetCoreTrademeStats.ClassLib.Models {
	public abstract class TrademeListing {
		[Key]
		public int ListingId { get; set; }
	}
}