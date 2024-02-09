using API.Setup;
using Application.Interfaces.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
//using MongoDB.Bson.IO;
using Newtonsoft.Json;

namespace API.Setup.Filters
{
    public class ATMCheckFilter : ActionFilterAttribute
    {

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var licensingService = context.HttpContext.RequestServices.GetService<ILicensingService>();
            var groupService = context.HttpContext.RequestServices.GetService<IGroupService>();

            if (licensingService == null || groupService == null)
            {
                context.Result = new UnauthorizedObjectResult(Responses.GetUnauthorizedResponse("ATM error"));
                return;
            }

            var user = context.HttpContext.User;
            if (user.Identity != null && user.Identity.IsAuthenticated && user.Identity.Name != null)
            {
                var activeATM = user.FindFirst("ActiveATM")?.Value;
                if (activeATM == null)
                {
                    context.Result = new UnauthorizedObjectResult(Responses.GetUnauthorizedResponse("User ATM inactive"));
                    return;
                }

                if (!licensingService.IsValidATM(activeATM))
                {
                    context.Result = new UnauthorizedObjectResult(Responses.GetUnauthorizedResponse("License ATM invalid"));
                    return;
                }

                if (!await groupService.UserCanAccessATMAsync(activeATM))
                {
                    context.Result = new UnauthorizedObjectResult(Responses.GetUnauthorizedResponse("User ATM unauthorised access"));
                    return;
                }
            }

            base.OnActionExecuting(context);
        }
    }
}