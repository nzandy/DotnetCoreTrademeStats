using System.Linq;
using System.Collections.Generic;
using DotnetCoreTrademeStats.ClassLib.Models;

namespace DotnetCoreTrademeStats.ClassLib.Repositories {
	public interface IRentalListingRepository {
		IQueryable<RentalListing> GetRentalListings();
		IQueryable<RentalListing> GetRentalListingsInLocality(int localityId);
		void AddRentalListing(RentalListing listing);
		void SaveChanges();
		IEnumerable<District> GetDistrictsInLocality(int localityId);
	}
}