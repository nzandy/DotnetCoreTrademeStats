using System.IO;
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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
				.Build();

			var host = new WebHostBuilder()	
				.UseKestrel()
				.UseConfiguration(config)
				.ConfigureServices(s => s.AddSingleton<IConfigurationRoot>(config))
				.ConfigureLogging(f => {
					f.AddConsole()
					.AddDebug();
				})
				.UseContentRoot(Directory.GetCurrentDirectory())
				.UseIISIntegration()
				.UseStartup<Startup>()
				.Build();

			host.Run();
		}
	}
}
