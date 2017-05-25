using DotnetCoreTrademeStats.ClassLib.Models;
using Microsoft.Extensions.Logging;

namespace DotnetCoreTrademeStats.ClassLib.Connectors {
	public class TrademeRentalConnector : TrademeAPIPaginatedConnector<RentalListing>, IRentalConnector {
		public TrademeRentalConnector(ILogger logger) : base(logger) {
		}
	}
}
	
	
