namespace Application.Interfaces.Security
{
	/// <summary>
	/// Check group rights 
	/// </summary>
	public interface IGroupService
	{
		public Task<bool> UserCanAccessATMAsync(string activeATM);

	}
}	