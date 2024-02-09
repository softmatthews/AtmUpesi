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
    public class EmailerHubNotificationhandler : INotificationHandler<EmailerNotification>
    {
        
        private readonly IHubContext<EmailerHub> _hubContext;
        private readonly IMediator _mediator;

		public EmailerHubNotificationhandler(IHubContext<EmailerHub> hubContext, IMediator mediator )
        {
             
			_hubContext = hubContext;
            _mediator = mediator;
		}        
         
        public async Task Handle(EmailerNotification notification, CancellationToken cancellationToken)
        {
        
           
            var st = notification;
            
            if (notification.NotificationType !=null)
            {

                var result = "notifiation typ";
			await _hubContext.Clients.All.SendAsync("LoadStatistics",result);
            
            }

        }

    }

}