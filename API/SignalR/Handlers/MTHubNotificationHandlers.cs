using API.SignalR.Hubs;
using Application.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace API.SignalR.Handlers
{

	public class MTTransactionMessageHandler: INotificationHandler<TransactionMessage>
	{
		private readonly IHubContext<MTHub> _hubContext;

		public MTTransactionMessageHandler(IHubContext<MTHub> hubContext)
		{
			_hubContext = hubContext;
		}

		public async Task Handle(TransactionMessage message, CancellationToken cancellationToken)
		{
			await _hubContext.Clients.All.SendAsync("ReceiveTransaction", message);
		}
	}

	public class MTTransactionLogHandler : INotificationHandler<TransactionLogNotification>
	{
		private readonly IHubContext<MTHub> _hubContext;

		public MTTransactionLogHandler(IHubContext<MTHub> hubContext)
		{
			_hubContext = hubContext;
		}

		public async Task Handle(TransactionLogNotification notification, CancellationToken cancellationToken)
		{
			await _hubContext.Clients.All.SendAsync("ReceiveLogUpdate", notification);
		}
	}
	
}