using Domain.User.Group;

namespace Domain.Settings
{
	/// <summary>
	/// Multi ATM enabler
	/// </summary>
    public class ATM
	{
		public int Id { get; set; }
		public string Identifier { get; set; } = null!;
		public ICollection<GroupATM> Groups { get; set; } = new List<GroupATM>();

	}
}