using Microsoft.AspNetCore.Identity;

namespace Domain.User.Account
{
    /// <summary>
    /// Class to handle Many to Many relationship between Accounts and Users
    /// </summary>
    public class AccountUsers
    {
        public int Id { get; set; }
        public AppUser User { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public Account Account { get; set; } = null!;
        public int AccountId { get; set; }
        public string? LastModifiedDate { get; set; } = new DateTime().ToLongTimeString();
        public DateTime CreatedAt { get; set; } = new DateTime();

    }
}