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
    public class GroupRepository : Repository<Group>, IGroupRepository
    {
        public GroupRepository(DbContext context) : base(context)
        {
        }

        public async Task<Group?> GetGroupByNameAsync(string groupName)
        {
            var group = await _entities
              .Where(v => v.Name == groupName)
              .FirstOrDefaultAsync();

            return group;
        }
        public async Task<Group?> GetGroupByIdAsync(int? groupId)
        {
            var group = await _entities
              .Where(v => v.Id == groupId)
              .FirstOrDefaultAsync();

            return group;
        }

        public async Task<IEnumerable<Group>> GetAllGroupsAsync()
        {
            return await _entities
                .Include(x => x.GroupUsers)
                      //.Include(x => x.GroupNewss)
                      .OrderByDescending(d => d.GroupUsers.Count())
                       .ToListAsync();
        }

    }
}