using Application.Core.Notifications;
using Application.Core.Wrappers;
using Application.Interfaces;
using Domain.Enums;
//using DurableTask.Core;
using Elsa.ActivityResults;
using Elsa.Services;
using Elsa.Services.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Application.Activities.Transactions
{
    public class GetTransactionMessageDetailsActivity : Activity
    {
        private readonly IMediator _mediator;
        private readonly ILogger<GetTransactionMessageDetailsActivity> _logger;

        public GetTransactionMessageDetailsActivity(IMediator mediator, ILogger<GetTransactionMessageDetailsActivity> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public override string? Name { get => "Transaction Message Details"; }

        public override string? DisplayName { get => "Transaction Message Details"; }

        protected override async ValueTask<IActivityExecutionResult> OnExecuteAsync(ActivityExecutionContext context)
        {
            TransactionMessage? message = context.GetVariable<TransactionMessage>("TransactionMessage");
            if (message == null)
            {
                _logger.LogError("Transaction message processed by {@Actor} is not present", "SYSTEM");
                return Fault("Transaction message is null");
            }
           
            return Done();
        }

    }
}
