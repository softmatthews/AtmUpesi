using Application.Activities.General;
using Application.Activities.Emailer;
using Application.Activities.Transactions;
using Application.Core.Notifications;
using Application.Core.Wrappers;
using Application.Orchestrations.Extensions;
using DurableTask.Core;
using Elsa.Activities.Console;
using Elsa.Activities.ControlFlow;
using Elsa.Activities.Workflows.Workflow;
using Elsa.Builders;
using Elsa.Events;
using Elsa.Models;

namespace Application.Orchestrations.Transactions
{
    public class WithdrawWorkflow : IWorkflow
    {

        public void Build(IWorkflowBuilder builder)
        {
            builder
                            .WithPersistenceBehavior(WorkflowPersistenceBehavior.ActivityExecuted)
                            .SetVariable<TransactionMessage>("TransactionMessage", context => context.GetInput<TransactionMessage>())
                            .SaveWorkflowContext(true)
                            .Then<GetRelatedLicensingActivity>()
                            .Then<GetTransactionMessageDetailsActivity>()
                            .ForEach
                            (setup => setup.GetVariable<List<string>>("PREPROCESSING")!, iterator =>
                            {
                                iterator.Then<PreprocessingActivity>();
                            }
                            )
                            .ForEach
                            (setup => setup.GetVariable<List<string>>("WITHDRAW")!, iterator =>
                            {
                                //iterator.Then<SpoolerActivity>();
                            })
                            .ForEach
                            (setup => setup.GetVariable<List<string>>("NOTIFICATIONS")!, iterator =>
                            {
                                //iterator.Then<EmailerActivity>();
                            })
                            
                            .ForEach
                            (setup => setup.GetVariable<List<string>>("POSTPROCESSING")!, iterator =>
                            {
                                iterator.Then<PostprocessingActivity>();
                            })
                            .WithId("WithdrawWorkflow")
                            .WithDisplayName("Withdraw Workflow")
                            .Build();
        }

    }
}