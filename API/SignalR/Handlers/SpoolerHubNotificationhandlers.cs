using MediatR;
using Microsoft.AspNetCore.Authorization;
using Application.Core.Notifications;
using Microsoft.AspNetCore.SignalR;
using API.Controllers;
using Application.Interfaces;
using Newtonsoft.Json;
using API.SignalR.Hubs;

namespace API.SignalR.Handlers
{
	public class SpoolerHubNotificationhandlers: INotificationHandler<SpoolerNotification>
    {
        
        private readonly IHubContext<SpoolerHub> _hubContext;
         private readonly IMediator _mediator;

		public SpoolerHubNotificationhandlers(IHubContext<SpoolerHub> hubContext, IMediator mediator)
        {
            
			_hubContext = hubContext;
            _mediator = mediator;

		}        
         
        public async Task Handle(SpoolerNotification notification, CancellationToken cancellationToken)
        {
        
           
            var st = notification;

            
            if (notification.NotificationType !=null)
            {
                var result = "";
			await _hubContext.Clients.All.SendAsync("LoadStatistics",result);
             
            }

        }

    }

}