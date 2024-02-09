
using API.SignalR.Hubs;

namespace API.Setup.Configurations
{
    internal static class EndpointsConfiguration
    {
        internal static void Configure(IEndpointRouteBuilder builder)
        {
            /**
            * Hubs
            */
            builder.MapHub<EmailerHub>("/hub/EmailerHub");
            builder.MapHub<SpoolerHub>("/hub/SpoolerHub");
            builder.MapHub<ISOHub>("/hub/ISOHub");
            builder.MapHub<MTHub>("/hub/MTHub");
            builder.MapHub<ISOWorkflowHub>("/hub/ISOWorkflowHub");

               builder.MapHub<NotificationHub>("/hub/NotificationHub");

            /**
            * Misc.
            */
            builder.MapFallbackToController("Index", "Fallback");


            /**
            * Ref: https://timdeschryver.dev/blog/maybe-its-time-to-rethink-our-project-structure-with-dot-net-6?tldr=1
            */

            /** 
            * Major
            */
            builder.MapControllers();

            /**
            * Customizations
            * ex: builder.MapGet("/orders/{orderId}", GetOrder.Handle);
            */
        }
    }
}
