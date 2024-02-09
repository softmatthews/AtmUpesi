//using Application.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Application.Interfaces.Security;
using API.Setup;

namespace API.Setup.Filters
{
    public class LicenseCheckFilter : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            var licensingService = context.HttpContext.RequestServices.GetService<ILicensingService>();

            if (licensingService == null)
            {
                context.Result = new UnauthorizedObjectResult(Responses.GetUnauthorizedResponse("License error"));
                return;
            }

            if (actionDescriptor != null)
            {
                var licenseCheckAttribute = actionDescriptor.MethodInfo.GetCustomAttributes(typeof(LicenseCheckAttribute), false).FirstOrDefault() as LicenseCheckAttribute;

                if (licenseCheckAttribute != null)
                {
                    if (licenseCheckAttribute.Feature != null && !licensingService.IsFeatureAvailable(licenseCheckAttribute.Feature))
                    {
                        context.Result = new UnauthorizedObjectResult(Responses.GetUnauthorizedResponse("Feature unavailable"));
                        return;
                    }

                    if (licenseCheckAttribute.Package != null && !licensingService.IsPackageAvailable(licenseCheckAttribute.Package))
                    {
                        context.Result = new UnauthorizedObjectResult(Responses.GetUnauthorizedResponse("Package unavailable"));
                        return;
                    }
                }
            }

            base.OnActionExecuting(context);
        }

    }
}