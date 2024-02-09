using Domain.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.User.Group;

namespace Domain.Repositories.Users.Groups
{
    public interface IGroupUsersRepository : IRepository<GroupUsers>
    {
        Task<IEnumerable<GroupUsers>> GetUserGroupsByGroupIdAsync(int? groupId);
    }
}
