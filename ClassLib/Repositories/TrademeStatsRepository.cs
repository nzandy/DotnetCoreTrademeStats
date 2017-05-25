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

		public void AddLocality(Locality locality) {
			if (!_context.Localities.Any(l => l.Id == locality.Id)) {
				// Only add if doesn't already exist.
				_context.Localities.Add(locality);
			}
		}

		public void AddDistrict(District district) {
			if (!_context.Localities.Any(d => d.Id == district.Id)) {
				// Only add if doesn't already exist.
				_context.Districts.Add(district);
			}
		}

		public void AddSuburb(Suburb suburb) {
			if (!_context.Suburbs.Any(s => s.Id == suburb.Id)) {
				// Only add if doesn't already exist.
				_context.Suburbs.Add(suburb);
			}
		}

		public void SaveChanges() {
			_context.SaveChanges();
		}

	}
}
