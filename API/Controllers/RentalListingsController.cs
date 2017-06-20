using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using DotnetCoreTrademeStats.ClassLib.Models;

namespace DotnetCoreTrademeStats.API.Controllers {

	[Route("api/[controller]")]
	public class RentalListingsController : Controller {
		private readonly TrademeStatsContext _dbContext;

		public RentalListingsController(TrademeStatsContext dbContext) {
			_dbContext = dbContext;
		}

		// GET: api/rentallistings
		public IEnumerable<RentalListing> Get(){
			return _dbContext.RentalListings.ToList();
		}

		// GET: api/authors/5
		[HttpGet("{id}")]
		public RentalListing Get(int listingId){
			return _dbContext.RentalListings.FirstOrDefault(l => l.Id == listingId);
		}

		[HttpPost]
		public IActionResult Post([FromBody]RentalListing value){
			_dbContext.RentalListings.Add(value);
			_dbContext.SaveChanges();
			return StatusCode(201, value);
		}
	}
}