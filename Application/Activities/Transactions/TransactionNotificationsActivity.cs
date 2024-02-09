using Application.Core.Notifications;
using Application.Core.Wrappers;
using Domain.Enums;
using Elsa.ActivityResults;
using Elsa.Services;
using Elsa.Services.Models;
using MediatR;
//using Microsoft.AspNetCore.Mvc.Formatters;

namespace Application.Activities.Transactions
{
    public class TransactionNotificationsActivity : Activity
    {
        private readonly IMediator mediator;

        public TransactionNotificationsActivity(IMediator mediator)
        {
            this.mediator = mediator;

        }

        public override string? DisplayName { get => "TransactionsNotifications"; }
        public override string? Name {get => "TransactionsNotifications"; }


		protected override async ValueTask<IActivityExecutionResult> OnExecuteAsync(ActivityExecutionContext context)
        {
            
            var detailsWrapper = context.GetVariable<TransactionDetailsWrapper>("TransactionMessageDetails");

            if (detailsWrapper == null)
			{
				return Fault("Transaction Notification not found for workflow");
			}
                        
                // await mediator.Send(new CreateNotification.Command(detailsWrapper.Message.TransactionID,detailsWrapper.Message.TransactionType,detailsWrapper.Value,ENotificationTypes.TRANSACTION));

            return Done();
        }
    }


}