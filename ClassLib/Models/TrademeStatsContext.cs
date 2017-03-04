using Microsoft.EntityFrameworkCore;

namespace DotnetCoreTrademeStats.ClassLib.Models {
	public class TrademeStatsContext : DbContext {
		
		public DbSet<RentalListing> RentalListings { get; set; }
		public DbSet<Agency> Agencies { get; set; }

		public TrademeStatsContext(DbContextOptions<TrademeStatsContext> options) : base (options){

		}
	}
}