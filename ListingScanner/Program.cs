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
		private static IConfigurationRoot _configuration;
		private static ILoggerFactory _loggerFactory;
		private static ILogger _logger;
		public static void Main(string[] args) {
			var services = new ServiceCollection();
			services.AddLogging();
			var connectionString = _configuration["DbContextSettings:ConnectionString"];
			services.AddDbContext<TrademeStatsContext>(
				opts => opts.UseNpgsql(connectionString));

			var serviceProvider = services.BuildServiceProvider();

			_logger = _loggerFactory.CreateLogger<Program>();

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

		private static void Configure(){
			var builder = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json");

			_configuration = builder.Build();
			_loggerFactory = new LoggerFactory()
			.AddConsole()
			.AddDebug();
		}
	}
}