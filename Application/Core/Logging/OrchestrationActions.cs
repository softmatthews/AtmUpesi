using Microsoft.Extensions.Logging;

namespace Application.Core.Logging
{
	public static class OrchestrationActions
	{
		private static readonly Action<ILogger, string, Exception?> failedOrchestration = LoggerMessage.Define<string>(
			LogLevel.Error,
			new EventId(4, "failedPipeline"),
			"Pipeline failure: {errorMessage}"
		);

		public static void FailedOrchestration(this ILogger logger, string failureMessage, Exception? ex = null) => failedOrchestration(logger, failureMessage, ex);
	}
}