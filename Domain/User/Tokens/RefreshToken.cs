using System.Security.Cryptography;

namespace Domain.User.Tokens
{
    /// <summary>
    /// a user in th ATM is not expected to stay assume 25mins max on the machine
    /// </summary>
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Revoked { get; set; }
        public string? ReplacedByToken { get; set; }
        public Guid RefreshTokenFamilyId { get; set; }
        public RefreshTokenFamily RefreshTokenFamily { get; set; } = null!;
        public bool IsActive => Revoked == null;
        public RefreshToken()
        {
            Token = GenerateRefreshToken();
            Created = DateTime.UtcNow;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}

