using Application.Core.Wrappers;
using Elsa.ActivityResults;
using Elsa.Services;
using Elsa.Services.Models;
using MediatR;

namespace Application.Activities.General
{
	public class PostprocessingActivity : Activity
	{
		private readonly IMediator _mediator;
		public PostprocessingActivity(IMediator mediator)
		{
			_mediator = mediator;
		}
		public override string? DisplayName { get => "Postprocessing"; }
        public override string? Name { get => "Postprocessing"; }

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

		private async Task HandleFeature(string license, TransactionDetailsWrapper detailsWrapper)
		{
			switch (license)
			{
				case "POSTTRANSACTIONS-CN00":
					// await _mediator.Send(new CreateTransaction.Command(detailsWrapper.Message));
					break;
				default:
					throw new Exception("Unknown license detected");
			}

			await Task.CompletedTask;
		}
	}
}