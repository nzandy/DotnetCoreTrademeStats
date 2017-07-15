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
		public Startup(IHostingEnvironment env, IConfigurationRoot config) {
			_configuration = config;
			_env = env;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) {
			services.AddLogging();

			string connectionString = _configuration["dbConnStr"];
			
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
