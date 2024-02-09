using Microsoft.Extensions.Logging;

namespace Application.Core.Logging
{
    public static class Emailer
    {
        private static readonly Action<ILogger, string, Exception?> sentEmail = LoggerMessage.Define<string>(
            LogLevel.Information,
            new EventId(1, "ProcessedISO"),
            "Sent Email for Transaction of type {Subfeature}"
        );

        private static readonly Action<ILogger, string, Exception?> failedEmail = LoggerMessage.Define<string>(
            LogLevel.Error,
            new EventId(1, "FailedISO"),
            "{errorMessage}"
        );

         private static readonly Action<ILogger, string, Exception?> deleteEmailSetting = LoggerMessage.Define<string>(
            LogLevel.Information,
            new EventId(1, "DeleteSetting"),
            "Deleted Email setting for Transaction of type {Subfeature}"
        );

                 private static readonly Action<ILogger, string, Exception?> deleteAccountSetting = LoggerMessage.Define<string>(
            LogLevel.Information,
            new EventId(1, "DeleteSetting"),
            "Deleted Account setting for user in {Subfeature}"
        );

        public static void SentEmail(this ILogger logger, string subfeature) => sentEmail(logger, subfeature, null);
        public static void FailedEmail(this ILogger logger, string failureMessage, Exception? ex = null) => failedEmail(logger, failureMessage, ex);
        public static void DeleteEmailSetting(this ILogger logger, string subfeature) => deleteEmailSetting(logger, subfeature, null);
        public static void DeleteAccountSetting(this ILogger logger, string subfeature) => deleteAccountSetting(logger, subfeature, null);
    
    }
}