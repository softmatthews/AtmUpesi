using System.Security.Claims;
using Application.Interfaces.Security;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Security
{
    public class UserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUsername()
        {

            if (_httpContextAccessor.HttpContext == null)
                return "";

            return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
        }

    }
}