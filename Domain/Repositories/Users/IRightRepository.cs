using Domain.User;

namespace Domain.Repositories.Users
{
    public interface IRightRepository : IRepository<Right>
    {
        Task<Right?> GetRightByNameAsync(string claimName);
        Task<Right?> GetRightByIdAsync(int claimId);
        Task<IEnumerable<Right>> GetAllRightsAsync();
    }
}
