using System.IO;
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetCoreTrademeStats.API {
	public class Program {
		public static void Main(string[] args) {
			var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

			if (String.IsNullOrEmpty(environment)){
				environment = "Development";
				Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
			}

			var config = new ConfigurationBuilder()
				.AddCommandLine(args)
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.AddJsonFile($"appsettings.{environment}.json", optional: true)
				.AddEnvironmentVariables()
				.Build();
			var host = new WebHostBuilder()	
				.UseKestrel()
				.UseConfiguration(config)
				.ConfigureServices(s => s.AddSingleton<IConfigurationRoot>(config))
				.UseContentRoot(Directory.GetCurrentDirectory())
				.UseIISIntegration()
				.UseStartup<Startup>()
				.Build();

			host.Run();
		}
	}
}
