using MediatR;
using Microsoft.AspNetCore.Authorization;
using Application.Core.Notifications;
using Microsoft.AspNetCore.SignalR;
using API.Controllers;
using Application.Interfaces;
using Newtonsoft.Json;
using API.SignalR.Hubs;
using Rebus.Messages;

namespace API.SignalR.Handlers
{
	public class NotificationHubNotificationhandlers : INotificationHandler<Notification>
    {
        
        private readonly IHubContext<NotificationHub> _hubContext;
         private readonly IMediator _mediator;

		public NotificationHubNotificationhandlers(IHubContext<NotificationHub> hubContext, IMediator mediator)
        {            
			_hubContext = hubContext;
            _mediator = mediator;
		}        
         
        public async Task Handle(Notification notification, CancellationToken cancellationToken)
        {
        
           
            var st = notification;

            await _hubContext.Clients.All.SendAsync("ReceiveNotification", notification);

            if (notification.Id >=0 )
            {  
   //         var result = await _mediator.Send(new GetNotificationCount.Query { });
			//await _hubContext.Clients.All.SendAsync("LoadNotifications", result.Value);
             
            }

        }

    }

}