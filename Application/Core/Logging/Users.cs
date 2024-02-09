using Microsoft.Extensions.Logging;

namespace Application.Core.Logging
{
    public static class  Users
    {
             
        private static readonly Action<ILogger, string, Exception?> deleteGroupSetting = LoggerMessage.Define<string>(
            LogLevel.Information,
            new EventId(1, "DeleteUserGroup"),
            "Deleted Group setting  {Subfeature}"
        );

        public static void DeleteGroupSetting(this ILogger logger, string subfeature) => deleteGroupSetting(logger, subfeature, null);

        
    }
}