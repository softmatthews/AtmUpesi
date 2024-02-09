using Microsoft.Extensions.Logging;

namespace Application.Core.Logging
{
	public static class NotificationsActions
	{
		
        private static readonly Action<ILogger, string, Exception?> duplicateTransaction = LoggerMessage.Define<string>(
            LogLevel.Warning,
            new EventId(4, "DuplicateTransaction"),
            "{errorMessage}"
        );
        public static void DuplicateTransaction(this ILogger logger, string failureMessage, Exception? ex = null) => duplicateTransaction(logger, failureMessage, ex);


        private static readonly Action<ILogger, string, Exception?> possibleDuplicate = LoggerMessage.Define<string>(
           LogLevel.Warning,
           new EventId(4, "PossibleDuplicateTransaction"),
           "{errorMessage}"
       );
        public static void PossibleDuplicateTransaction(this ILogger logger, string failureMessage, Exception? ex = null) => possibleDuplicate(logger, failureMessage, ex);


        private static readonly Action<ILogger, string, Exception?> transactionNotAcknowledged = LoggerMessage.Define<string>(
           LogLevel.Warning,
           new EventId(4, "TransactionNotAcknowledged"),
           "{errorMessage}"
       );
        public static void TransactionNotAcknowledged(this ILogger logger, string failureMessage, Exception? ex = null) => transactionNotAcknowledged(logger, failureMessage, ex);


    }
}