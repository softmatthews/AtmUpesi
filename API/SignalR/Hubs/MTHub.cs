using Application.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace API.SignalR.Hubs
{
    public class MTHub : Hub
	{
		private readonly IMediator _mediator;
		public MTHub(IMediator mediator)
		{
			_mediator = mediator;
		}
		public async Task SendDashboardUpdate()
		{
		}

		public override async Task OnConnectedAsync()
		{						
		}
	}
}