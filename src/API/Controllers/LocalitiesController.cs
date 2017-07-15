using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using DotnetCoreTrademeStats.ClassLib.Models;

namespace DotnetCoreTrademeStats.API.Controllers {

	[Route("api/[controller]")]
	public class LocalitiesController : Controller {

		private readonly TrademeStatsContext _dbContext;

		public LocalitiesController(TrademeStatsContext dbContext) {
			_dbContext = dbContext;
		}

		// GET: api/localities
		public IEnumerable<Locality> Get(){
			return _dbContext.Localities.ToList();
		}
	}
}