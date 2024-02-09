using Domain.Settings;

namespace Domain.User.Group
{
    /// <summary>
    /// Class to handle Many to Many relationship between ATM and Groups
    /// </summary>
    public class GroupATM
    {
        public int Id { get; set; }
        public int ATMId { get; set; }
        public ATM ATM { get; set; } = null!;
        public int GroupId { get; set; }
        public Group Group { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}