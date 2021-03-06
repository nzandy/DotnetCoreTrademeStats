﻿using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

		public IQueryable<RentalListing> GetRentalListingsInLocality(int localityId){

			var listings = GetRentalListings();

			// 100 = All.
			if (localityId == 100){
				return listings;
			}

			// Fetch districts in given locality (as we do not have a direct link from RentalListing)
			IEnumerable<int> districtsInLocality = GetDistrictsInLocality(localityId).Select(d => d.Id);
			return listings.Where(l => districtsInLocality.Contains(l.DistrictId));
		}

		public IEnumerable<District> GetDistrictsInLocality(int localityId){
			Locality locality = _context.Localities.Include(l => l.Districts).FirstOrDefault(l => l.Id == localityId);
			if (locality != null){
				return locality.Districts;
			}
			return new List<District>();
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

		public LocalityRentalStatistic GetRentalStatsForLocality(int localityId){
			List<RentalListing> listingsInLocality = GetRentalListingsInLocality(localityId).ToList();
			LocalityRentalStatistic stats = new LocalityRentalStatistic();
			if (listingsInLocality.Any()){
				stats.ListingCount = listingsInLocality.Count;
				stats.AverageRentPerWeek = listingsInLocality.Average(l => l.RentPerWeek);
				stats.AverageRentPerRoom = listingsInLocality.Average(l => (l.RentPerWeek / l.Bedrooms));
				stats.HighestRentPerWeek = listingsInLocality.Max(l => l.RentPerWeek);
				stats.LowestRentPerWeek = listingsInLocality.Min(l => l.RentPerWeek);
			}

			return stats;
		}

		public void SaveChanges() {
			_context.SaveChanges();
		}

	}
}
