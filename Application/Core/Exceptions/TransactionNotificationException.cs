using Domain.Enums;

namespace Application.Core.Exceptions
{
    public class TransactionNotificationException : Exception
    {
        public string TransactionId { get; set; }
        public string TransactionType { get; set; }
        public ENotificationTypes NotificationType { get; set; }
        public string NotificationMessageDetails { get; set; }
        
        public TransactionNotificationException(string transactionId, string transactionType , ENotificationTypes notificationType,   string notificationMessageDetails)
        {
            TransactionId = transactionId;
            TransactionType = transactionType;
            NotificationType = notificationType;
            NotificationMessageDetails = notificationMessageDetails;
        }

    }

}