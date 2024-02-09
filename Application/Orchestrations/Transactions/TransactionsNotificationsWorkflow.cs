using Application.Activities.Transactions;
using Application.Core.Notifications;
using Elsa.Builders;
using Elsa.Models;

namespace Application.Orchestrations.Transactions
{
    public class TransactionsNotificationsWorkflow : IWorkflow
    {
        public void Build(IWorkflowBuilder builder)
        {
            builder
                .WithPersistenceBehavior(WorkflowPersistenceBehavior.ActivityExecuted)
                .SetVariable<TransactionMessage>("TransactionNotification", context => context.GetInput<TransactionMessage>())
                .SaveWorkflowContext(true)
                .Then<TransactionNotificationsActivity>()
                .WithId("TransactionsNotificationsWorkflow")
                .WithDescription("Transaction Notification Workflow")
                .Build();          
        }
    }
}