using Application.Core.Notifications;
using Domain.Enums;

namespace Application.Core.Wrappers
{
    public class TransactionDetailsWrapper
    {
        public TransactionMessage Message { get; set; } = null!;
        public Dictionary<ETransactionMessageDetails, List<string>> Value { get; set; } = new();
    }
}