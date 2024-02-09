using Microsoft.Extensions.Logging;

namespace Application.Core.Logging
{
    public static class Spooler
    {
        private static readonly Action<ILogger, string, Exception?> spooledTransaction = LoggerMessage.Define<string>(
            LogLevel.Information,
            new EventId(1, "ProcessedISO"),
            "Spooled Transaction of type {Subfeature}"
        );

        private static readonly Action<ILogger, string, Exception?> failedSpool = LoggerMessage.Define<string>(
            LogLevel.Error,
            new EventId(1, "FailedISO"),
            "{errorMessage}"
        );

        public static void SpooledTransaction(this ILogger logger, string subfeature) => spooledTransaction(logger, subfeature, null);
        public static void FailedSpool(this ILogger logger, string failureMessage, Exception? ex = null) => failedSpool(logger, failureMessage, ex);
    }
}