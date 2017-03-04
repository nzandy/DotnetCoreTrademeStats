using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using DotnetCoreTrademeStats.ClassLib.Connectors;
using DotnetCoreTrademeStats.ClassLib.Repositories;
using DotnetCoreTrademeStats.ClassLib.Models;

namespace DotnetCoreTrademeStats.ListingScanner.ConsoleApp {
	public class Program {
		static public IConfigurationRoot Configuration { get; set; }

		public static void Main(string[] args) {
			ILogger _logger;

			var builder = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json");

			Configuration = builder.Build();

			var services = new ServiceCollection();
			services.AddLogging();
			var connectionString = Configuration["DbContextSettings:ConnectionString"];
			services.AddDbContext<TrademeStatsContext>(
				opts => opts.UseNpgsql(connectionString));

			var serviceProvider = services.BuildServiceProvider();

			_logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger<Program>();

			var rentalConnector = new TrademeRentalConnector("v1/Search/Property/Rental.json?", _logger);
			TrademeStatsContext context = serviceProvider.GetService<TrademeStatsContext>();
			TrademeStatsRepository respository = new TrademeStatsRepository(context);

			while (true) {

				IEnumerable<RentalListing> rentalListings = rentalConnector.GetListings();
				Stopwatch sw = Stopwatch.StartNew();
				sw.Start();
				foreach (var rentalListing in rentalListings) {
					_logger.LogDebug($"Adding listing ID: {rentalListing.ListingId}");
					respository.AddRentalListing(rentalListing);
				}

				respository.SaveChanges();
				sw.Stop();
				_logger.LogDebug($"Finished fetching listings, took {sw.Elapsed.TotalSeconds} seconds. Press enter to exit.");
				Thread.Sleep(600000);
			}
		}
	}
}