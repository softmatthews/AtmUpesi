using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.SignalR;
using Application.Core.License;
using Domain.User;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistence;
using Persistence.MongoDB;
using Persistence.SeedWork;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.Configure<HostOptions>(hostOptions =>
                    {
                        hostOptions.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.Ignore;
                    });
                });

            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            // SQL configurations
            try
            {
                var context = services.GetRequiredService<DataContext>();
                await context.Database.MigrateAsync();
                await SeedData(services);
            }
            catch (Exception ex)
            {
                Console.Write(ex + "An error occurred during migration");
            }

            // NOSQL configurations
            Configuration.MongoConfigure();

            // License configurations
            try
            {
                string encryptedLicensePath = "D:\\Upesi\\License\\license.kp";

                LicenseCache.Instance.LoadLicense(encryptedLicensePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex + "An error occurred during license setup");
            }
            try
            {
                await host.RunAsync();
            }
            catch (Exception exc)
            {
                string dt = exc.ToString();
            }


        }

        private static async Task SeedData(IServiceProvider services)
        {
            var context = services.GetRequiredService<DataContext>();
            var userManager = services.GetRequiredService<UserManager<AppUser>>();
            var environment = services.GetRequiredService<IWebHostEnvironment>();
            if (environment.IsDevelopment())
            {
                await SeedUsers.Seed(context, userManager);
                await SeedSettings.Seed(context);


            }
            else
            {
                await SeedUsers.Seed(context, userManager);
                await SeedSettings.Seed(context);

            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
                    Host.CreateDefaultBuilder(args)
                                    .ConfigureWebHostDefaults(webBuilder =>
                                    {
                                        webBuilder.UseStartup<Startup>();

                                    });
    }
}