using System;
using System.IO;
using DevExpress.AspNetCore;
using DevExpress.AspNetCore.Reporting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using DevExpress.XtraReports.Web.Extensions;
using SevenReport.Services;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting.Server.Features;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Server;
using System.Diagnostics;

namespace SevenReport
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

	

		void ProcessException(Exception ex, string message)
		{
			// Log exceptions here. For instance:
			System.Diagnostics.Debug.WriteLine("[{0}]: Exception occured. Message: '{1}'. Exception Details:\r\n{2}",
				DateTime.Now, message, ex);
		}

		public IConfiguration Configuration { get; }
		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			//services.AddControllersWithViews();
			services.AddDevExpressControls();
			services.AddMvcCore();
			services.AddSession();
			services.AddScoped<ReportStorageWebExtension, NewSevenReport>();
			services
				.AddMvc()
				.AddDefaultReportingControllers()
				//.AddNewtonsoftJson()
				.SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
			services.ConfigureReportingServices(configurator =>
			{
				configurator.ConfigureReportDesigner(designerConfigurator =>
				{
					designerConfigurator.RegisterDataSourceWizardConfigFileConnectionStringsProvider();
					designerConfigurator.RegisterDataSourceWizardJsonConnectionStorage<CustomDataSourceWizardJsonDataConnectionStorage>(true);
					designerConfigurator.RegisterObjectDataSourceWizardTypeProvider<ObjectDataSourceWizardCustomTypeProvider>();
				});
				configurator.ConfigureWebDocumentViewer(viewerConfigurator =>
				{
					viewerConfigurator.UseCachedReportSourceBuilder();
					viewerConfigurator.RegisterJsonDataConnectionProviderFactory<CustomJsonDataConnectionProviderFactory>();
				});
			});
			// In production, the Angular files will be served from this directory
			services.AddSpaStaticFiles(configuration =>
			{
				configuration.RootPath = "ClientApp/dist";
			});

			services.AddHttpsRedirection(options =>
			{
				options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
				options.HttpsPort = 5001;
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
		{

			var reportingLogger = loggerFactory.CreateLogger("DXReporting");
			DevExpress.XtraReports.Web.ClientControls.LoggerService.Initialize((exception, message) =>
			{
				var logMessage = $"[{DateTime.Now}]: Exception occurred. Message: '{message}'. Exception Details:\r\n{exception}";
				reportingLogger.LogError(logMessage);
			});
			DevExpress.XtraReports.Configuration.Settings.Default.UserDesignerOptions.DataBindingMode = DevExpress.XtraReports.UI.DataBindingMode.ExpressionsAdvanced;
			DevExpress.XtraReports.Web.ClientControls.LoggerService.Initialize(ProcessException);


			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				// EventLog.WriteEntry("Info", "Debug ");
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
				// EventLog.WriteEntry("Info", "Release 1");
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			if (!env.IsDevelopment())
			{
				app.UseSpaStaticFiles();
			}

			app.UseRouting();
			app.UseSession();
			app.UseDevExpressControls();
			System.Net.ServicePointManager.SecurityProtocol |= System.Net.SecurityProtocolType.Tls12;
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller}/{action=Index}/{id?}");
			});

			app.UseSpa(spa =>
			{
				// To learn more about options for serving an Angular SPA from ASP.NET Core,
				// see https://go.microsoft.com/fwlink/?linkid=864501

				spa.Options.SourcePath = "ClientApp";

				if (env.IsDevelopment())
				{
					spa.UseAngularCliServer(npmScript: "start");
				}
			});

			app.UseCors("AllowCorsPolicy");

			var configurationBuilder = new ConfigurationBuilder()
					.SetBasePath("D:/DW/converted")
					.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
					.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
					.AddInMemoryCollection(new Dictionary<string, string>()
					{
						[$"ConnectionStrings:SevenDesarrollo"] = "XpoProvider=MSSqlServer;Server=cerezo;User ID=desarrollo;Password=Desarrollo2018;Database=sevendesarrollo;Trusted_Connection=False;MultipleActiveResultSets=true;"
						// [$"ConnectionStrings:SevenDesarrollo"] = "XpoProvider=MSSqlServer;Server=190.85.14.66,1933;User ID=XXX;Password=Sistemas123;Database=SevenOphelia;Trusted_Connection=False;MultipleActiveResultSets=true;"
					})
					.AddEnvironmentVariables();
			var reportCustomConfiguration = configurationBuilder.Build();

			var globalConnectionStrings = reportCustomConfiguration
							.GetSection("ConnectionStrings")
							.AsEnumerable(true)
							.ToDictionary(x => x.Key, x => x.Value);
			DevExpress.DataAccess.DefaultConnectionStringProvider.AssignConnectionStrings(globalConnectionStrings);
		}
		public class ServerAddressesMiddleware
		{
			private readonly IFeatureCollection _features;
			public ServerAddressesMiddleware(RequestDelegate _, IServer server)
			{
				_features = server.Features;
			}

			public async Task Invoke(HttpContext context)
			{
				// fetch the addresses
				var addressFeature = _features.Get<IServerAddressesFeature>();
				var addresses = addressFeature.Addresses;

				// Write the addresses as a comma separated list
				await context.Response.WriteAsync(string.Join(",", addresses));
			}
		}
	}
}
