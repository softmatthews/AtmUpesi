using System.Linq.Expressions;
using Application.Orchestrations.Transactions;
using Elsa.Models;
using Elsa.Persistence;
using Elsa.Persistence.EntityFramework.Core;
using Elsa.Persistence.Specifications;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace API.SignalR.Hubs
{
	public class IsFinishedSpecification : Specification<WorkflowInstance>
	{
		public override Expression<Func<WorkflowInstance, bool>> ToExpression()
		{
			return x => x.WorkflowStatus == WorkflowStatus.Finished;
		}
	}

	public class IsRunningSpecification : Specification<WorkflowInstance>
	{
		public override Expression<Func<WorkflowInstance, bool>> ToExpression()
		{
			return x => x.WorkflowStatus == WorkflowStatus.Running;
		}
	}
	public class ISOWorkflowHub : Hub
	{
		private readonly IMediator _mediator;
		private readonly IWorkflowInstanceStore _store;

		public ISOWorkflowHub(IMediator mediator, IWorkflowInstanceStore store)
		{
			_mediator = mediator;
			_store = store;
		}
		public override async Task OnConnectedAsync()
		{
			var completedItems = await _store.CountAsync(new IsFinishedSpecification());
			var runningItems = await _store.CountAsync(new IsRunningSpecification());

			await Clients.All.SendAsync("CompletedItemCount", completedItems);
			await Clients.All.SendAsync("RunningItemsCount", runningItems);
		}
	}
}

