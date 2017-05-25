using System;
using System.IO;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using DotnetCoreTrademeStats.ClassLib.Connectors;
using DotnetCoreTrademeStats.ClassLib.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;


namespace DotnetCoreTrademeStats.Utilities.FetchTrademeLocalities {
	class Program {
		static void Main(string[] args) {
			var serviceCollection = new ServiceCollection();
			ConfigureServices(serviceCollection);
			IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
			FetchTrademeLocalities listingScanner = serviceProvider.GetService<FetchTrademeLocalities>();
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
			services.AddTransient<FetchTrademeLocalities>();


			var connectionString = configuration["DbContextSettings:ConnectionString"];
			services.AddDbContext<TrademeStatsContext>(
				opts => opts.UseNpgsql(connectionString));
		}

		private static IConfigurationRoot GetConfiguration(){
			return new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json", optional: false)
			.Build();
		}
	}

	public class FetchTrademeLocalities {
		ILogger _logger;
		TrademeStatsContext _context;
		public FetchTrademeLocalities(ILoggerFactory loggerFactory, TrademeStatsContext context){
			_logger = loggerFactory.CreateLogger<FetchTrademeLocalities>();
			_context = context;
		}
		public async Task Run(){
			TrademeLocalityConnector connector = new TrademeLocalityConnector(_logger);
			var listings = connector.GetListings();
		}
	}

	
}
