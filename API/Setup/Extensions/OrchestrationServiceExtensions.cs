using Application.Activities.General;
using Application.Activities.Transactions;
using Application.Orchestrations.Transactions;
//using DurableTask.Core;
//using DurableTask.SqlServer;
using Elsa.Persistence.EntityFramework.Core.Extensions;
using Elsa.Persistence.EntityFramework.SqlServer;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Setup.Extensions
{
    /// <summary>
    /// Orchestration framework services
    /// </summary>
    public static class OrchestrationServiceExtensions
    {
        public static IServiceCollection AddOrchestrationServices(this IServiceCollection services, IConfiguration config)
        {
            services
                .AddElsa(elsa => elsa
                    .UseEntityFrameworkPersistence(ef => ef.UseSqlServer(config.GetConnectionString("SqlContext")!, typeof(DataContext)))
                    .AddConsoleActivities()
                    .AddWorkflow(typeof(DepositWorkflow))
                    .AddWorkflow(typeof(WithdrawWorkflow))
                    .AddWorkflow(typeof(FailedTransactionWorkflow))
                     .AddWorkflow(typeof(TransactionsNotificationsWorkflow))
                    .AddActivitiesFrom(typeof(DepositWorkflow).Assembly)
                );

            return services;
        }
    }
}