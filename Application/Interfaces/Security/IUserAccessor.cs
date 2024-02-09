using Domain.User;

namespace Application.Interfaces.Security
{
    /// <summary>
    /// General interface to access current user properties
    /// </summary>
    public interface IUserAccessor
    {
        string GetUsername();
    }
}