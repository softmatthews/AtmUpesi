using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Setup.Services.Interfaces;
using Application.Interfaces.Settings;
using Domain.User;
using Domain.User.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Persistence;

namespace API.Setup.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly IUserClaimsPrincipalFactory<AppUser> _userClaimsPrincipalFactory;
        private readonly DataContext _context;
       // private readonly ISettings _settings;

        public TokenService( IConfiguration config, IUserClaimsPrincipalFactory<AppUser> userClaimsPrincipalFactory, DataContext context)
        {
            //ISettings settings,
            //_settings = settings;
            _context = context;
            _config = config;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        }
        public bool IsValidRefreshToken(RefreshTokenFamily family, string refreshToken)
        {

            var possibleToken = family.Tokens.Where(x => x.Token == refreshToken).FirstOrDefault();

            if (possibleToken == null || possibleToken.Revoked != null)
            {
                return false;
            }

            return true;
        }

        public async Task<string> CreateToken(AppUser user)
        {
            var principal = await _userClaimsPrincipalFactory.CreateAsync(user);
            var claims = principal.Claims.ToList();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["TokenKey"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var sessionTime = 50;// await _settings.GetSessionTimeoutAsync();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(sessionTime > 60 || sessionTime < 0 ? 20 : sessionTime),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<string> CreateRefreshToken(AppUser user)
        {
            var newFamily = new RefreshTokenFamily() { User = user };
            var newRefreshToken = new RefreshToken()
            {
                RefreshTokenFamily = newFamily,
            };

            newFamily.Tokens.Add(newRefreshToken);

            await _context.RefreshTokenFamilies.AddAsync(newFamily);
            await _context.SaveChangesAsync();

            return newRefreshToken.Token;
        }
        public async Task InvalidateUserRefreshTokenFamilies(AppUser user)
        {
            var families = await GetActiveRefreshTokenFamilies(user);
            await InvalidateUserRefreshTokenFamilies(user, families);
        }
        public async Task InvalidateUserRefreshTokenFamilies(AppUser user, List<RefreshTokenFamily> families)
        {
            foreach (var family in families)
            {
                family.Valid = false;
                _context.RefreshTokenFamilies.Update(family);
            }

            await _context.SaveChangesAsync();
        }
        public async Task InvalidateRefreshTokenFamily(RefreshTokenFamily family)
        {
            family.Valid = false;
            _context.RefreshTokenFamilies.Update(family);
            await _context.SaveChangesAsync();
        }
        public async Task<List<RefreshTokenFamily>> GetActiveRefreshTokenFamilies(AppUser user) =>
                await _context.RefreshTokenFamilies
                        .Where(x => x.UserId == user.Id && x.Valid)
                        .Include(x => x.Tokens)
                        .ToListAsync();

        public async Task<RefreshToken> CycleRefreshTokenAsync(AppUser user, RefreshTokenFamily family)
        {
            RefreshToken? mostPreviousToken = family.Tokens.OrderByDescending(x => x.Created).FirstOrDefault();
            RefreshToken refreshToken = new()
            {
                RefreshTokenFamily = family
            };

            if (mostPreviousToken != null)
            {
                mostPreviousToken.Revoked = refreshToken.Created;
                mostPreviousToken.ReplacedByToken = refreshToken.Token;
            }
            family.Tokens.Add(refreshToken);
            _context.RefreshTokenFamilies.Update(family);

            await _context.SaveChangesAsync();

            return refreshToken;
        }


    }
}
