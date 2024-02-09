using Application.Core.Notifications;
using Application.Interfaces.Security;
using Elsa.ActivityResults;
using Elsa.Services;
using Elsa.Services.Models;

namespace Application.Activities.General
{
    public class GetRelatedLicensingActivity : Activity
    {
        private readonly ILicensingService _licensingService;
        public GetRelatedLicensingActivity(ILicensingService licensingService)
        {
            _licensingService = licensingService;
        }

        public override string? DisplayName { get => "Licensing"; }
        public override string? Name { get => "Licensing"; }



        protected override IActivityExecutionResult OnExecute(ActivityExecutionContext context)
        {
            context.SetVariable("PREPROCESSING", _licensingService.GetRelatedFeatures("PRE"));
            context.SetVariable("SPOOLER", _licensingService.GetRelatedFeatures("SPOOLER"));
            context.SetVariable("EMAILER", _licensingService.GetRelatedFeatures("EMAILER"));
            context.SetVariable("POSTPROCESSING", _licensingService.GetRelatedFeatures("POST"));
            context.SetVariable("NOTIFICATIONS", _licensingService.GetRelatedFeatures("POST"));
            return Done();
        }

    }
}