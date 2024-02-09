namespace Domain.User.Tokens
{
    public class RefreshTokenFamily
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = null!;
        public AppUser User { get; set; } = null!;
        public bool Valid { get; set; } = true;
        public DateTime Created { get; set; }
        public DateTime Expires { get; set; }
        public bool IsExpired => DateTime.Now > Expires;
        public List<RefreshToken> Tokens { get; set; } = new();

        public RefreshTokenFamily()
        {
            Created = DateTime.UtcNow;
            Expires = DateTime.UtcNow.AddHours(24);
        }

    }
}