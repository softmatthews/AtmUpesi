using API.SignalR.Hubs;
using Elsa.Events;
using MediatR;
using Microsoft.AspNetCore.SignalR;
namespace API.SignalR.Handlers
{
	public class OrchestrationNotification
	{
		public string? InstanceID { get; set; }
		public string? ActivityName { get; set; }
	}

	public class A : INotificationHandler<Elsa.Events.WorkflowBlueprintLoaded>
	{
		private readonly IHubContext<ISOWorkflowHub> _context;
		public A(IHubContext<ISOWorkflowHub> context)
		{
			_context = context;
		}
		public async Task Handle(WorkflowBlueprintLoaded notification, CancellationToken cancellationToken)
		{
			var activities = notification.WorkflowBlueprint.Activities.Select(activity => activity.Type);
			await _context.Clients.All.SendAsync("ReceiveWorkflowName", notification.WorkflowBlueprint.Name);
		}
	}
	public class B : INotificationHandler<Elsa.Events.WorkflowExecuting>
	{
		private readonly IHubContext<ISOWorkflowHub> _context;
		public B(IHubContext<ISOWorkflowHub> context)
		{
			_context = context;
		}
		public async Task Handle(WorkflowExecuting notification, CancellationToken cancellationToken)
		{
			await _context.Clients.All.SendAsync("ReceiveWorkflowStartedId", notification.WorkflowExecutionContext.WorkflowInstance.Id);
		}
	}


	public class C : INotificationHandler<ActivityExecuting>
	{
		private readonly IHubContext<ISOWorkflowHub> _hubContext;

		public C(IHubContext<ISOWorkflowHub> hubContext)
		{
			_hubContext = hubContext;
		}

		public async Task Handle(ActivityExecuting message, CancellationToken cancellationToken)
		{
			OrchestrationNotification orc = new() { ActivityName = message.Activity.Name, InstanceID = message.WorkflowExecutionContext.WorkflowInstance.Id };
			await _hubContext.Clients.All.SendAsync("ReceiveActivityExecuting", orc);
		}

	}



	public class D : INotificationHandler<Elsa.Events.ActivityExecuted>
	{

		private readonly IHubContext<ISOWorkflowHub> _hubContext;
		public D(IHubContext<ISOWorkflowHub> context)
		{
			_hubContext = context;

		}
		public async Task Handle(ActivityExecuted notification, CancellationToken cancellationToken)
		{

			OrchestrationNotification orc = new() { ActivityName = notification.Activity.Name, InstanceID = notification.WorkflowExecutionContext.WorkflowInstance.Id };
			await _hubContext.Clients.All.SendAsync("ReceiveActivityExecuted", orc);
		}
	}



	public class E : INotificationHandler<WorkflowCompleted>
	{
		private readonly IHubContext<ISOWorkflowHub> _hubContext;
		public E(IHubContext<ISOWorkflowHub> context)
		{
			_hubContext = context;

		}
		public async Task Handle(WorkflowCompleted notification, CancellationToken cancellationToken)
		{
			await _hubContext.Clients.All.SendAsync("ReceiveWorkflowCompletedId", notification.WorkflowExecutionContext.WorkflowInstance.Id);
		}
	}

	public class OchestrationActivityResumingNotificationHandler : INotificationHandler<ActivityResuming>
	{
		private readonly IHubContext<ISOHub> _hubContext;

		public OchestrationActivityResumingNotificationHandler(IHubContext<ISOHub> hubContext)
		{
			_hubContext = hubContext;
		}

		public async Task Handle(ActivityResuming notification, CancellationToken cancellationToken)
		{
			Console.WriteLine(notification?.WorkflowExecutionContext?.WorkflowInstance?.CurrentActivity?.Input?.ToString() + "activityResuming");
			Console.WriteLine(notification?.WorkflowExecutionContext?.WorkflowInstance?.CurrentActivity?.Input?.ToString() + "activityResuming");
			Console.WriteLine(notification?.WorkflowExecutionContext.WorkflowInstance.Name + "activity Resuming" + notification?.Activity.DisplayName + "details here " + notification?.Activity.Name);
			await _hubContext.Clients.All.SendAsync("ReceiveTransaction", notification?.WorkflowExecutionContext);
		}
	}



}