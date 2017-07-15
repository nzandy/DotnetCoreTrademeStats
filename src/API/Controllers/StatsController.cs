using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using DotnetCoreTrademeStats.ClassLib.Models;
using DotnetCoreTrademeStats.ClassLib.Repositories;

namespace DotnetCoreTrademeStats.API.Controllers {

	[Route("api/[controller]")]
	public class StatsController : Controller {

		private readonly IRentalListingRepository _repository;

		public StatsController(IRentalListingRepository repository) {
			_repository = repository;
		}

		[HttpGet("/api/stats/locality/{localityId}")]
		public LocalityRentalStatistic GetStatisticForLocality(int localityId){
			return _repository.GetRentalStatsForLocality(localityId);
		}
	}
}