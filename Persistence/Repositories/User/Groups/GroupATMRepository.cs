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
    public class GroupATMRepository : Repository<GroupATM>, IGroupATMRepository
    {
        public GroupATMRepository(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<GroupATM>> GetGroupATMsAsync(int? groupId)
        {
            var groupATMs = await _entities
              .Where(v => v.GroupId == groupId)
              .Include(v => v.ATM)
              .ToListAsync();

            return groupATMs;
        }
        public async Task<IEnumerable<GroupATM>> GetAllGroupATMsAsync()
        {
            var groupATMs = await _entities
            .OrderByDescending(d => d.CreatedAt)
            .ToListAsync();
            return groupATMs;
        }

        //public async Task<IEnumerable<GroupClaim>> GetAllGroups()
        //{
        //    return await _entities
        //              .OrderByDescending(d => d.CreatedAt)
        //               .ToListAsync();
        //}

    }
}