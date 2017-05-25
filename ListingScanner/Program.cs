using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using DotnetCoreTrademeStats.ClassLib.Connectors;
using DotnetCoreTrademeStats.ClassLib.Repositories;
using DotnetCoreTrademeStats.ClassLib.Models;

namespace DotnetCoreTrademeStats.ListingScanner.ConsoleApp {
	public class Program {
		public static void Main(string[] args) {
			var serviceCollection = new ServiceCollection();
			ConfigureServices(serviceCollection);
			IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
			ListingScanner listingScanner = serviceProvider.GetService<ListingScanner>();
			Task.Run(() => listingScanner.Run()).Wait();
		}

		private static void ConfigureServices(IServiceCollection services){
			ILoggerFactory loggerFactory = new LoggerFactory()
			.AddConsole()
			.AddDebug();

			services.AddSingleton(loggerFactory);
			services.AddLogging();

			IConfigurationRoot configuration = GetConfiguration();
			services.AddSingleton(configuration);
			services.AddTransient<ListingScanner>();

			services.AddDbContext<TrademeStatsContext>(
				opts => opts.UseNpgsql(configuration["DbContextSettings:ConnectionString"]));
		}

		private static IConfigurationRoot GetConfiguration(){
			return new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json", optional: false)
			.Build();
		}
	}

	public class ListingScanner{
		ILogger _logger;
		TrademeStatsContext _context;
		public ListingScanner(ILoggerFactory loggerFactory, TrademeStatsContext context){
			_logger = loggerFactory.CreateLogger<ListingScanner>();
			_context = context;
		}

		public async Task Run(){
			var rentalConnector = new TrademeRentalConnector(_logger);
			TrademeStatsRepository respository = new TrademeStatsRepository(_context);

			while (true) {

				IEnumerable<RentalListing> rentalListings = rentalConnector.GetListings();
				Stopwatch sw = Stopwatch.StartNew();
				sw.Start();
				foreach (var rentalListing in rentalListings) {
					_logger.LogDebug($"Adding listing ID: {rentalListing.Id}");
					respository.AddRentalListing(rentalListing);
				}

				respository.SaveChanges();
				sw.Stop();
				_logger.LogInformation($"Finished fetching listings, took {sw.Elapsed.TotalSeconds} seconds. Press enter to exit.");
				Thread.Sleep(600000);
			}
		}
	}
}