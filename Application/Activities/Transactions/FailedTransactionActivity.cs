using Application.Core.Notifications;
using Elsa.ActivityResults;
using Elsa.Services;
using Elsa.Services.Models;
using MediatR;
//using Microsoft.AspNetCore.Mvc.Formatters;

namespace Application.Activities.Transactions
{
    public class FailedTransactionActivity : Activity
    {
        private readonly IMediator mediator;

        public FailedTransactionActivity(IMediator mediator)
        {
            this.mediator = mediator;

        }

        public override string? DisplayName { get => "FailedTransactionTransaction"; }
        public override string? Name {get => "FailedTransactionTransaction";}


		protected override async ValueTask<IActivityExecutionResult> OnExecuteAsync(ActivityExecutionContext context)
        {
            var failedTransaction = context.GetVariable<TransactionFailed>("TransactionFailedTransaction");
			if (failedTransaction == null)
			{
				return Fault("Transaction failed transaction not found for workflow");
			}

            //await mediator.Send();

            return Done();
        }
    }


}