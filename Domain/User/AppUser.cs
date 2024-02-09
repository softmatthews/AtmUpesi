using Domain.User.Account;
using Domain.User.Group;
using Microsoft.AspNetCore.Identity;

namespace Domain.User
{
	/// <summary>
	/// Extends IdentityUser to utiliz the powerful funtionalities 
	/// </summary>
	public class AppUser : IdentityUser
	{
		public string DisplayName { get; set; } = null!;
		public string? Bio { get; set; } = null!;
		public string? RegistrationBank { get; set; } // the initial bank of registration
		public bool OTPAuthentication { get; set; }
		public string? OTPKey { get; set; } = null!;
		public DateTime CurrentLogin { get; set; } = DateTime.Now;
		public DateTime LastLogin { get; set; } = DateTime.Now;
		public string? LastAccessedPages { get; set; }
        public DateTime LastNotificationReadTime { get; set; } = DateTime.Now;
        public ICollection<GroupUsers> GroupUsers { get; set; } = new List<GroupUsers>();

		//Users accounts... a user can have  multiple accounts
		 public ICollection<AccountUsers> AccountUsers { get; set; } = new List<AccountUsers>();

		//userchanges tracks  the actions of the user in the app	
		public ICollection<UserChange> UserChanges { get; set; } = new List<UserChange>();
	}

}