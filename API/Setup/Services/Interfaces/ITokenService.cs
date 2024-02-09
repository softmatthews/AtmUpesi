using Domain.User;
using Domain.User.Tokens;

namespace API.Setup.Services.Interfaces
{
    public interface ITokenService
    {

        bool IsValidRefreshToken(RefreshTokenFamily family, string refreshToken);
        Task<string> CreateToken(AppUser user);
        Task<string> CreateRefreshToken(AppUser user);
        Task InvalidateUserRefreshTokenFamilies(AppUser user);
        Task InvalidateUserRefreshTokenFamilies(AppUser user, List<RefreshTokenFamily> families);
        Task InvalidateRefreshTokenFamily(RefreshTokenFamily family);
        Task<List<RefreshTokenFamily>> GetActiveRefreshTokenFamilies(AppUser user);
        Task<RefreshToken> CycleRefreshTokenAsync(AppUser user, RefreshTokenFamily family);

    }
}