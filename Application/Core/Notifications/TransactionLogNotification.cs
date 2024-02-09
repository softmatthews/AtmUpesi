using MediatR;

namespace Application.Core.Notifications
{
    public class TransactionLogNotification : INotification
    {
        public string Message { get; set; } = null!;
        public string Level { get; set; } = null!;
        public DateTime TimeStamp { get; } = DateTime.Now;
    }
}