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
    public class GroupUsersRepository : Repository<GroupUsers>, IGroupUsersRepository
    {
        public GroupUsersRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<GroupUsers>> GetUserGroupsByGroupIdAsync(int? groupId)
        {
            var userGroup = await _entities
             .Where(v => v.GroupId == groupId)
             .ToListAsync();
            return userGroup;
        }



    }
}