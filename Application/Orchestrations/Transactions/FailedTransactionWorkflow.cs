using Application.Activities.Transactions;
using Application.Core.Notifications;
using Elsa.Builders;
using Elsa.Models;

namespace Application.Orchestrations.Transactions
{
    public class FailedTransactionWorkflow : IWorkflow
    {
        public void Build(IWorkflowBuilder builder)
        {
            builder
                .WithPersistenceBehavior(WorkflowPersistenceBehavior.ActivityExecuted)
                .SetVariable<TransactionFailed>("TransactionFailedTransaction", context => context.GetInput<TransactionFailed>())
                .SaveWorkflowContext(true)
                .Then<FailedTransactionActivity>()
                .WithId("FailedTransactionWorkflow")
                .WithDescription("Failed Transaction Workflow")
                .Build();          
        }
    }
}