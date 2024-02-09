using Application.Core.Wrappers;
using Elsa.ActivityResults;
using Elsa.Services;
using Elsa.Services.Models;
//using MongoDB.Driver;

namespace Application.Activities.General
{
	public class PreprocessingActivity : Activity
	{
		public PreprocessingActivity()
		{
			this.Name = "Preprocessing";
			this.DisplayName = "Preprocessing";
		}

		public override string? DisplayName { get => "Preprocessing"; }
        public override string? Name { get => "Preprocessing"; }

        private static async Task HandleFeature(string feature, TransactionDetailsWrapper input)
		{
			switch (feature)
			{
				case "PRETESTSIMPLE-CN00":
					await Task.Delay(10000);
					break;
				case "PRETESTERROR-CN00":
					await Task.Delay(10000);
					break;
				case "PRETESTLONG-CN00":
					await Task.Delay(30000);
					break;
				default:
					throw new Exception($"Unexpected feature type: {feature}");
			}
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