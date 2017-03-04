using System.Linq;
using DotnetCoreTrademeStats.ClassLib.Models;

namespace DotnetCoreTrademeStats.ClassLib.Repositories {
	public interface IRentalListingRepository {
		IQueryable<RentalListing> GetRentalListings();
		void AddRentalListing(RentalListing listing);
		void SaveChanges();
	}
}