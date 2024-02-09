using Application.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace API.SignalR.Hubs
{
    public class EmailerHub : Hub
	{
		private readonly IMediator _mediator;
		public EmailerHub(IMediator mediator)
		{
			_mediator = mediator;
		}
		public override async Task OnConnectedAsync()
		{						
			
		}
	}
}