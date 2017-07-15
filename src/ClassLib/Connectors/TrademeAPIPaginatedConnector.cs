using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using DotnetCoreTrademeStats.ClassLib.Models;
using Newtonsoft.Json;
using System.Net.Http;

namespace DotnetCoreTrademeStats.ClassLib.Connectors{
	public class TrademeAPIPaginatedConnector<TEntity> : TrademeApiConnector<TEntity> where TEntity : TrademeListing {
		const int PageSize = 500;

		public TrademeAPIPaginatedConnector(ILogger logger) : base(logger){}

		public override IEnumerable<TEntity> GetListings(){
			var listings = new List<TEntity>();

				int numResults;
				int pageNum = 1;

				_logger.LogInformation($"Attempting to fetch {typeof(TEntity).Name} listings from {Client.BaseAddress}");
				_logger.LogInformation($"Page size is {PageSize}");

				do {
					string requestUri = GetRequestUri(pageNum);

					HttpResponseMessage response = Client.GetAsync(requestUri).Result;
					string json = response.Content.ReadAsStringAsync().Result;

					var listingResponse = JsonConvert.DeserializeObject<ListingContainer<TEntity>>(json, Settings);

					numResults = listingResponse.TotalCount;
					listings.AddRange(listingResponse.List);

					pageNum++;
				} while (PageSize * (pageNum - 1) < numResults);
				_logger.LogInformation($"Total count of listings: {numResults}");
				_logger.LogInformation($"Successfuly parsed {listings.Count} rental listings.");
				return listings;
		}

		private string GetRequestUri(int pageNum) {
			_logger.LogInformation($"Fetching page number {pageNum} of results");
			return string.Format($"{RelativeUri}rows={PageSize}&page={pageNum}");
		}
	}

}