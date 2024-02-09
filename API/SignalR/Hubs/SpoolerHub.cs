using Application.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace API.SignalR.Hubs
{
	public class SpoolerHub : Hub
	{
		private readonly IMediator _mediator;
		public SpoolerHub(IMediator mediator)
		{
			_mediator = mediator;
		}

		// From frontend
		public override async Task OnConnectedAsync()
		{						
		}
	}
} 