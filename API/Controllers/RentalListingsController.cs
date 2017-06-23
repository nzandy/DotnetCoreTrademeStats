using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using DotnetCoreTrademeStats.ClassLib.Models;
using DotnetCoreTrademeStats.ClassLib.Repositories;

namespace DotnetCoreTrademeStats.API.Controllers {

	[Route("api/[controller]")]
	public class RentalListingsController : Controller {
		private readonly IRentalListingRepository _repository;

		public RentalListingsController(TrademeStatsContext dbContext, IRentalListingRepository repository) {
			_repository = repository;
		}

		// GET: api/rentallistings
		public IEnumerable<RentalListing> Get(){
			return _repository.GetRentalListings();
		}

		[HttpGet("/api/rentallistings/locality/{localityId}")]
		public IEnumerable<RentalListing> GetByLocality(int localityId){
			return _repository.GetRentalListingsInLocality(localityId);
		}

		// GET: api/rentallistings/5
		[HttpGet("{id}")]
		public RentalListing Get(int listingId){
			return _repository.GetRentalListings().FirstOrDefault(l => l.Id == listingId);
		}

		[HttpPost]
		public IActionResult Post([FromBody]RentalListing value){
			_repository.AddRentalListing(value);
			_repository.SaveChanges();
			return StatusCode(201, value);
		}
	}
}