using System.Linq;
using DotnetCoreTrademeStats.ClassLib.Models;


namespace DotnetCoreTrademeStats.ClassLib.Repositories {

	public class TrademeStatsRepository : IRentalListingRepository {

		private readonly TrademeStatsContext _context;
		public TrademeStatsRepository(TrademeStatsContext context) {
			_context = context;
		}

		public IQueryable<RentalListing> GetRentalListings() {
			return _context.RentalListings;
		}

		public void AddRentalListing(RentalListing listing) {
			if (!_context.RentalListings.Any(l => l.Id == listing.Id)) {
				// Only add if doesn't already exist.
				_context.RentalListings.Add(listing);
			}
		}

		public void SaveChanges() {
			_context.SaveChanges();
		}

	}
}
