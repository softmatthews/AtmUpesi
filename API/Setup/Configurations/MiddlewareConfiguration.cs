using API.Setup.Middleware;

namespace API.Setup.Configurations
{
    internal static class MiddlewareConfiguration
    {
        internal static void Configure(IApplicationBuilder app)
        {
			app.UseMiddleware<ExceptionMiddleware>();

        }
    }
}