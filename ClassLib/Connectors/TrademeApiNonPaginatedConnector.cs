using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using DotnetCoreTrademeStats.ClassLib.Models;
using Newtonsoft.Json;
using System.Net.Http;

namespace DotnetCoreTrademeStats.ClassLib.Connectors{
	public class TrademeAPINonPaginatedConnector<TEntity> : TrademeApiConnector<TEntity> where TEntity : TrademeListing {
		const int PageSize = 500;

		public TrademeAPINonPaginatedConnector(ILogger logger) : base(logger){}

		public override IEnumerable<TEntity> GetListings(){
			var listings = new List<TEntity>();

			_logger.LogInformation($"Attempting to fetch {typeof(TEntity).Name} listings from {Client.BaseAddress}");

			try {
			HttpResponseMessage response = Client.GetAsync(RelativeUri).Result;
			string json = response.Content.ReadAsStringAsync().Result;

			return JsonConvert.DeserializeObject<List<TEntity>>(json, Settings);
			} catch (Exception e){
				_logger.LogError("Error while fetching results");
				return new List<TEntity>();
			}			
		}
	}

}