using System.ComponentModel.DataAnnotations;

namespace DotnetCoreTrademeStats.Models{
	public abstract class TrademeListing {
		[Key]
		public int ListingId { get; set; }
	}
}