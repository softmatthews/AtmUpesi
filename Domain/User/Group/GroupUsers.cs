using Microsoft.AspNetCore.Identity;

namespace Domain.User.Group
{
    /// <summary>
    /// Class to handle Many to Many relationship between Group and Users
    /// </summary>
    public class GroupUsers
    {
        public int Id { get; set; }
        public AppUser User { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public Group Group { get; set; } = null!;
        public int GroupId { get; set; }
        public string? LastModifiedDate { get; set; } = new DateTime().ToLongTimeString();
        public DateTime CreatedAt { get; set; } = new DateTime();

    }
}