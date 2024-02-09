using Application.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace API.SignalR.Hubs
{
	public class NotificationHub : Hub
	{
		private readonly IMediator _mediator;
		public NotificationHub(IMediator mediator)
		{
			_mediator = mediator;
		}

		// From frontend
        public async Task SendNotificationUpdate()
        {

        }

        public override async Task OnConnectedAsync()
        {
            //var result = await _mediator.Send(new GetNotificationCount.Query { });
            await Clients.Caller.SendAsync("LoadNotifications", "1");
        }
    }
} 