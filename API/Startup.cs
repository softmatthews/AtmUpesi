using System.Text.Json.Serialization;
using API.SignalR;
using API.SignalR.Hubs;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Hosting;
//using DinkToPdf;
//using DinkToPdf.Contracts;
using API.Setup.Filters;
using API.Setup.Extensions;
using API.Setup.Services;
using API.Setup.Configurations;
using Asp.Versioning.ApiExplorer;

namespace API
{
	public class Startup
	{
		private readonly IConfiguration _config;
		private readonly IWebHostEnvironment _environment;

		public Startup(IConfiguration config, IWebHostEnvironment environment)
		{
			_config = config;
			_environment = environment;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			try
			{
				services.AddControllers(opt =>
				{
					var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
					opt.Filters.Add(new AuthorizeFilter(policy));
					opt.Filters.Add<ApiExceptionFilterAttribute>();
					opt.Filters.Add<LicenseCheckFilter>();
				})
				.AddJsonOptions(opt =>
			 {
				 opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
			 });

				if (_environment.IsDevelopment())
				{
					services.ConfigureApplicationCookie(options =>
					{
						options.Cookie.SameSite = SameSiteMode.None;
					});
				}
				else
				{
					services.ConfigureApplicationCookie(options =>
					{
						options.Cookie.SameSite = SameSiteMode.Strict;
					});
				}
				services.AddFluentValidationClientsideAdapters();
				//services.AddValidatorsFromAssemblyContaining<Validators.CreateISOValidator>();

				services.AddSecurityServices(_config);
				services.AddApplicationServices(_config);
				services.AddOrchestrationServices(_config);
				services.AddIdentityServices(_config);
				services.AddHostedService<ProcessFilesBackGroundService>();

				services.AddSignalR();
			}
			catch (Exception exc)
			{
				string jop = exc.Message.ToString();
			}

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
		{
			/**
			* Order matters
			* Security -> Routing -> Static files -> Auth
			*/
			MiddlewareConfiguration.Configure(app);

			SecurityConfiguration.Configure(app);
			// if (env.IsDevelopment())
			// {
			DevConfiguration.Configure(app, provider);
			//}			

			app.UseRouting();

			// Will look for wwwroot
			app.UseDefaultFiles();
			app.UseStaticFiles();


			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(EndpointsConfiguration.Configure);
		}
	}
}