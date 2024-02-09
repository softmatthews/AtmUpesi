using System.Collections.ObjectModel;
using System.Data;
using API.Setup.Configurations;
using API.SignalR;
using API.SignalR.Handlers;
using Application.Core;
using Application.Core.Behaviors;
using Application.Features.User;
using Application.Interfaces;
//using Application.Interfaces.Settings;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Domain.Repositories;
using Infrastructure.Security;
using Infrastructure.Settings;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Persistence;
using Persistence.MongoDB;
using Persistence.Repositories;
using Serilog;
using Serilog.Configuration;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;

namespace API.Setup.Extensions
{
    /// <summary>
    /// Service extensions
    /// </summary>
    public static class ApplicationServiceExtensions
    {
        /// <summary>
        /// Persistence and layer interconnectedness setup
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        /// <param name="config">IConfiguration</param>
        /// <returns></returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            // API
            services.ConfigureOptions<SwaggerConfiguration>();
            services
                .AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
                .AddApiExplorer(options =>
            {
                options.SubstituteApiVersionInUrl = true;
                options.SubstitutionFormat = "VV";
                options.GroupNameFormat = "'v'VVV";
            });
            services.AddSwaggerGen();


            // Databases
            services.AddDbContext<DataContext>(options =>
                    options.UseSqlServer(config.GetConnectionString("SqlContext"))
            );

            services.Configure<MongoDbSettings>(config.GetSection("MongoDBSettings"));
            services.AddSingleton<IMongoDbSettings>(serviceProvider =>
                    serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);

            // Logging
            var columnOptions = new ColumnOptions
            {
                AdditionalColumns = new Collection<SqlColumn>
                {
                    new() { DataType = SqlDbType.NVarChar, ColumnName = "Actor", AllowNull = true },
                    new() { DataType = SqlDbType.NVarChar, ColumnName = "Package", AllowNull = true },
                    new() { DataType = SqlDbType.NVarChar, ColumnName = "Feature" , AllowNull = true},
                    new() { DataType = SqlDbType.NVarChar, ColumnName = "Subfeature", AllowNull = true },
                }
            };
            columnOptions.Store.Remove(StandardColumn.Properties);
            columnOptions.Store.Add(StandardColumn.LogEvent);
            columnOptions.Id.DataType = SqlDbType.BigInt;
            

            var serilogLogger = new LoggerConfiguration()
                    .MinimumLevel.Override("Microsoft", LevelAlias.Maximum)
                    .MinimumLevel.Override("Elsa", LogEventLevel.Warning)
                    .AuditTo.MSSqlServer(config.GetConnectionString("SqlContext"), new MSSqlServerSinkOptions
                    {
                        TableName = "Logs",
                    },
                    columnOptions: columnOptions,
                    restrictedToMinimumLevel: LogEventLevel.Information
                    )
                    .CreateLogger();

            var devLogger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();


            var customLogger = new LoggerConfiguration()
                    .MinimumLevel.Information()
                     .WriteTo.CustomSink()
                        .CreateLogger();


            services.AddSingleton(serviceProvider =>
            {
                return LoggerFactory.Create(builder =>
                {
                    builder.AddSerilog(serilogLogger);
                    var env = serviceProvider.GetRequiredService<IHostEnvironment>();
                    // if (env.IsDevelopment())
                    // {
                    builder.AddSerilog(devLogger);
                    builder.AddSerilog(customLogger);
                    // }
                });
            });



            // TODO: Eventually remove, will be part of API resources when publishing
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyMethod().AllowAnyHeader().AllowCredentials().WithOrigins("http://localhost:3000");
                    policy.AllowAnyMethod().AllowAnyHeader().AllowCredentials().WithOrigins("http://localhost:4200");
                });
            });

            // SignalR 
            services.AddCors(options =>
            {
                options.AddPolicy("SignalRClientPolicy", policy =>
                {
                    policy.AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowCredentials()
                                .WithOrigins("http://localhost:4200");
                });
            });
            services.AddSignalR();
                       
            services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(typeof(GetGroupUsers.Handler).Assembly); });
            services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(typeof(EmailerHubNotificationhandler).Assembly); });
            services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(typeof(SpoolerHubNotificationhandlers).Assembly); });

            services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(typeof(NotificationHubNotificationhandlers).Assembly); });

            services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(typeof(CustomSink).Assembly); });

            // Follows order below
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionNotificationExceptionBehaviour<,>));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));
            // Add auth here
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LicenseCheckBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));
                        
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<Application.Interfaces.Settings.ISettings, Settings>();


            services.AddTransient<IAuthenticationService, GoogleAuthenticationService>();

            services.AddSignalR();

            return services;
        }

    }
}