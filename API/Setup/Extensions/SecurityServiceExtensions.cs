using API.Setup.Filters;
using Application.Core.License;
using Application.Interfaces;
using Application.Interfaces.Security;
using Infrastructure.Security;

namespace API.Setup.Extensions
{
    public static class SecurityServiceExtensions
    {
        public static IServiceCollection AddSecurityServices(this IServiceCollection services, IConfiguration config)
        {
            // Licensing
            services.AddSingleton(LicenseCache.Instance);
            services.AddTransient<ILicensingService, LicensingService>();
            services.AddScoped<LicenseCheckFilter>();

            //services.AddTransient<IGroupService, GroupService>();
            services.AddScoped<ATMCheckFilter>();

            // User access
            services.AddScoped<IUserAccessor, UserAccessor>();

            // User logging
            services.AddScoped<LogUserActionFilter>();

            return services;
        }
    }
}