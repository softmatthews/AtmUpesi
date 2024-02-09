using API.SignalR.Hubs;
using Application.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace API.SignalR.Handlers
{

	public class TransactionMessageHandler: INotificationHandler<TransactionMessage>
	{
		private readonly IHubContext<ISOHub> _hubContext;

		public TransactionMessageHandler(IHubContext<ISOHub> hubContext)
		{
			_hubContext = hubContext;
		}

		public async Task Handle(TransactionMessage message, CancellationToken cancellationToken)
		{
			await _hubContext.Clients.All.SendAsync("ReceiveTransaction", message);
		}
	}

	public class TransactionLogHandler : INotificationHandler<TransactionLogNotification>
	{
		private readonly IHubContext<ISOHub> _hubContext;

		public TransactionLogHandler(IHubContext<ISOHub> hubContext)
		{
			_hubContext = hubContext;
		}

		public async Task Handle(TransactionLogNotification notification, CancellationToken cancellationToken)
		{
			await _hubContext.Clients.All.SendAsync("ReceiveLogUpdate", notification);
		}
	}
}