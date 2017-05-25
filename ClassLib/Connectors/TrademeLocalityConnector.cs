using DotnetCoreTrademeStats.ClassLib.Models;
using Microsoft.Extensions.Logging;

namespace DotnetCoreTrademeStats.ClassLib.Connectors {
	public class TrademeLocalityConnector : TrademeAPINonPaginatedConnector<Locality>, IRentalConnector {
		public TrademeLocalityConnector(ILogger logger) : base(logger) {
		}
	}
}
	
	
