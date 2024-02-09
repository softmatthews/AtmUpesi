using Microsoft.Extensions.Logging;

namespace Application.Core.Logging
{
	public static class TransactionActions
	{
		private static readonly Action<ILogger, string, Exception?> capturedISO = LoggerMessage.Define<string>(
			LogLevel.Information,
			new EventId(1, "ProcessedISO"),
			"Captured ISO Transaction of type {Subfeature}"
		);

		private static readonly Action<ILogger, string, Exception?> partialISO = LoggerMessage.Define<string>(
			LogLevel.Warning,
			new EventId(2, "PartialISO"),
			"Partial success of reading ISO file - {successRate}"
		);

		private static readonly Action<ILogger, string, Exception?> failedISO = LoggerMessage.Define<string>(
			LogLevel.Error,
			new EventId(3, "FailedISO"),
			"{errorMessage}"
		);

		private static readonly Action<ILogger, string, Exception?> criticallyFailedISO = LoggerMessage.Define<string>(
			LogLevel.Critical,
			new EventId(4, "CriticallyFailedISO"),
			"{errorMessage}"
		);

		public static void CapturedISO(this ILogger logger, string subfeature) => capturedISO(logger, subfeature, null);
		public static void PartialISO(this ILogger logger, string successRate) => partialISO(logger, successRate, null);
		public static void FailedISO(this ILogger logger, string failureMessage, Exception? ex = null) => failedISO(logger, failureMessage, ex);
		public static void CriticallyFailedISO(this ILogger logger, string failureMessage, Exception? ex = null) => criticallyFailedISO(logger, failureMessage, ex);



		private static readonly Action<ILogger, string, Exception?> capturedMT = LoggerMessage.Define<string>(
			LogLevel.Information,
			new EventId(1, "ProcessedMT"),
			"Captured MT Transaction of type {Subfeature}"
		);

		private static readonly Action<ILogger, string, Exception?> partialMT = LoggerMessage.Define<string>(
			LogLevel.Warning,
			new EventId(2, "PartialMT"),
			"Partial success of reading MT file - {successRate}"
		);

		private static readonly Action<ILogger, string, Exception?> failedMT = LoggerMessage.Define<string>(
			LogLevel.Error,
			new EventId(3, "FailedMT"),
			"{errorMessage}"
		);

		public static void CapturedMT(this ILogger logger, string subfeature) => capturedMT(logger, subfeature, null);
		public static void PartialMT(this ILogger logger, string successRate) => partialMT(logger, successRate, null);
		public static void FailedMT(this ILogger logger, string failureMessage, Exception? ex = null) => failedMT(logger, failureMessage, ex);
		
    }
}