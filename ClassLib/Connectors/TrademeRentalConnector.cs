using DotnetCoreTrademeStats.ClassLib.Models;
using Microsoft.Extensions.Logging;

namespace DotnetCoreTrademeStats.ClassLib.Connectors {
	public class TrademeRentalConnector : TrademeApiConnector<RentalListing>, IRentalConnector {
		public TrademeRentalConnector(string relativeUri, ILogger logger) : base(relativeUri, logger) {
		}
	}
}
	
	
