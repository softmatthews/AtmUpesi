using Microsoft.AspNetCore.Identity;

namespace Domain.User.Group
{
    /// <summary>
    /// Class to handle Many to Many relationship between Group and Claim
    /// </summary>
    public class GroupRight
    {
        public int Id { get; set; }
        public Right Right { get; set; } = null!;
        public int RightId { get; set; }
        public Group Group { get; set; } = null!;
        public int GroupId { get; set; }
        public DateTime CreatedAt { get; set; } = new DateTime();

    }
}
