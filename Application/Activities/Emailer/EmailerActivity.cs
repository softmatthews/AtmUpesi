using Application.Core.License;
using Application.Core.Wrappers;
using Domain.Enums;
using Application.Interfaces;
using Elsa.ActivityResults;
using Elsa.Services;
using Elsa.Services.Models;
using MediatR;
//using MongoDB.Driver.Core.Misc;

namespace Application.Activities.Emailer
{
    public class EmailerActivity : Activity
    {
        private readonly IMediator mediator;

        public EmailerActivity(IMediator _mediator)
        {
            this.mediator = _mediator;
        }
        public override string? DisplayName { get => "Emailer"; }
        public override string? Name { get => "Emailer"; }


        private async Task HandleFeature(string feature, TransactionDetailsWrapper input)
        {

            switch (feature)
                {
                    case "EMAILER-CN00":
                        //await mediator.Send();
                        break;
                    default:
                        throw new Exception("Unknown license detected");
                }

            await Task.CompletedTask;
        }

        protected override async ValueTask<IActivityExecutionResult> OnExecuteAsync(ActivityExecutionContext context)
        {
            var license = context.CurrentScope?.Variables.Data["CurrentValue"]?.ToString();
            var detailsWrapper = context.GetVariable<TransactionDetailsWrapper>("TransactionMessageDetails");
            if (license == null)
            {
                return Done();
            }
            if (detailsWrapper == null)
            {
                return Fault("Details wrapper not found");
            }
            await HandleFeature(license, detailsWrapper);
            return Done();
        }
    }
}