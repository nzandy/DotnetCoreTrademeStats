using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using DotnetCoreTrademeStats.ClassLib.Models;
using DotnetCoreTrademeStats.ClassLib.Repositories;

namespace DotnetCoreTrademeStats.API {
	public class Startup {
		private IConfigurationRoot _configuration;
		private IHostingEnvironment _env;
		private ILogger<Startup> _logger;
		public Startup(IHostingEnvironment env, ILogger<Startup> logger) {
			_env = env;
			_logger = logger;
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				.AddEnvironmentVariables();

				_configuration = builder.Build();
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) {
			services.AddLogging();

			string connectionString = _configuration["dbConnStr"];
			_logger.LogInformation("Connection string: {0}", connectionString);
			
			services.AddDbContext<TrademeStatsContext>(
				opts => opts.UseNpgsql(connectionString));	
			// Add framework services.
			services.AddMvc();

			services.AddCors(o => o.AddPolicy("MyPolicy", builder => {
				builder.AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader();
			}));

			services.AddTransient<IRentalListingRepository, TrademeStatsRepository>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddConsole(_configuration.GetSection("Logging"));
			loggerFactory.AddDebug();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseBrowserLink();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}
			app.UseCors("MyPolicy");
			app.UseStaticFiles();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
