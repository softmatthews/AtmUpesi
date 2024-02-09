using Domain.Repositories;
using Domain.Repositories.Users;
using Domain.Settings;
using Domain.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Persistence.Repositories.User
{
    public class RightsRepository : Repository<Right>, IRightRepository
    {
        public RightsRepository(DbContext context) : base(context)
        {

        }

        public async Task<Right?> GetRightByNameAsync(string ClaimName)
        {
            var rightsClaims = await _entities
              .Where(v => v.Name == ClaimName)
              .FirstOrDefaultAsync();

            return rightsClaims;
        }

        public async Task<IEnumerable<Right>> GetAllRightsAsync()
        {
            return await _entities
                      .OrderByDescending(d => d.CreatedAt)
                       .ToListAsync();
        }
        public async Task<Right?> GetRightByIdAsync(int claimId)
        {
            var claim = await _entities
                 .Where(v => v.Id == claimId)
                      .OrderByDescending(d => d.CreatedAt)
                      .FirstOrDefaultAsync();

            return claim;
        }
    }
}