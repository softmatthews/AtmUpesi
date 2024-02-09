using Microsoft.AspNetCore.Mvc;

namespace API.Setup
{
    internal class Responses
    {
        internal static ProblemDetails GetUnauthorizedResponse(string details)
        {
            return new ProblemDetails()
            {
                Type = "https://tools.ietf.org/html/rfc7235#section-3.1",
                Status = StatusCodes.Status401Unauthorized,
                Title = "Unauthorized",
                Detail = details
            };
        }
    }
}