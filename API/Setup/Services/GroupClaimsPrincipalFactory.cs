using System.Security.Claims;
using Domain.Enums;
using Domain.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Persistence;

namespace API.Setup.Services
{
    public class GroupClaimsPrincipalFactory : UserClaimsPrincipalFactory<AppUser, IdentityRole>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly DataContext _context;
        public GroupClaimsPrincipalFactory(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> options,
            DataContext context)
            : base(userManager, roleManager, options)
        {
            _context = context;
            _userManager = userManager;
        }
        public async override Task<ClaimsPrincipal> CreateAsync(AppUser user)
        {
            var principal = await base.CreateAsync(user);

            if (principal.Identity is ClaimsIdentity identity)
            {
                var groups = _context.UserGroups
                        .Include(g => g.Group)
                        .ThenInclude(gc => gc.GroupClaims)
                        .ThenInclude(gcc => gcc.Right)
                        .Where(ug => ug.UserId == user.Id);

                foreach (var userGroup in groups)
                {
                    foreach (var groupClaim in userGroup.Group.GroupClaims)
                    {
                        var claim = groupClaim.Right;
                        identity.AddClaim(new Claim(Enum.GetName(typeof(ERights), claim.Type)!, claim.Name));
                    }
                }
                identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
                identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
                if (user.RegistrationBank != null)
                {
                    identity.AddClaim(new Claim("ActiveATM", user.RegistrationBank));
                }
            }

            return principal;
        }
    }

}