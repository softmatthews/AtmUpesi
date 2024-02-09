using System.Diagnostics;
using Application.Interfaces;
using Application.Interfaces.Security;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Setup.Filters
{
    public class LogUserActionFilter : IActionFilter
    {
        private readonly IUserAccessor _userAccessor;
        private readonly ILogger<LogUserActionFilter> _logger;
        private Stopwatch _timer;

        // Add names of sensitive properties here
        private static readonly HashSet<string> SensitiveProperties = new()
        {
                "password",
        };

        public LogUserActionFilter(IUserAccessor userAccessor, ILogger<LogUserActionFilter> logger)
        {
            _userAccessor = userAccessor;
            _logger = logger;
            _timer = new Stopwatch();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _timer.Start();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _timer.Stop();
            var elapsedMilliseconds = _timer.ElapsedMilliseconds;
            if (elapsedMilliseconds > 1000)
            {
                var username = _userAccessor.GetUsername();
                var parameters = context.ModelState
                    .Where(p => !SensitiveProperties.Contains(p.Key.ToLower()))
                    .Select(p => $"{p.Key}: {p.Value?.RawValue}")
                    .ToArray();
                _logger.LogWarning($"User {username} executed action '{context.ActionDescriptor.DisplayName}' with parameters {string.Join(", ", parameters)} which took {elapsedMilliseconds} ms");
            }
        }

    }

}