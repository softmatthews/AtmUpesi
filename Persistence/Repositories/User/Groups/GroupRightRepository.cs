using Domain.Repositories;
using Domain.Repositories.Users.Groups;
using Domain.Settings;
using Domain.User.Group;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Persistence.Repositories.User.Groups
{
    public class GroupRightRepository : Repository<GroupRight>, IGroupRightRepository
    {
        public GroupRightRepository(DbContext context) : base(context)
        {
        }


        public async Task<IEnumerable<GroupRight>> GetGroupRightsAsync(int? groupId)
        {
            var groupClaims = await _entities
              .Where(v => v.GroupId == groupId)
              .Include(v => v.Right)
              .ToListAsync();

            return groupClaims;
        }
        public async Task<IEnumerable<GroupRight>> GetAllGroupClaimsAsync()
        {
            var groupClaims = await _entities
            .OrderByDescending(d => d.CreatedAt)
            .ToListAsync();

            return groupClaims;
        }

        //public async Task<IEnumerable<GroupClaim>> GetAllGroups()
        //{
        //    return await _entities
        //              .OrderByDescending(d => d.CreatedAt)
        //               .ToListAsync();
        //}

    }
}