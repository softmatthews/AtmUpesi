using Application.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace API.SignalR.Hubs
{
    public class ISOHub : Hub
	{
		private readonly IMediator _mediator;
		public ISOHub(IMediator mediator)
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