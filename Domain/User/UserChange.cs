using Microsoft.AspNetCore.Identity;

namespace Domain.User
{
    /// <summary>
    /// Tracking user changes
    /// </summary>
    public class UserChange
    {
        public int Id { get; set; }
        public AppUser User { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public string LastEditUserId { get; set; } = null!;
        public string Status { get; set; } = null!; 
        public string Action { get; set; } = null!; 
        public string? LastModifiedDate { get; set; } = new DateTime().ToLongTimeString();
        public DateTime CreatedAt { get; set; } = new DateTime();

    }
}