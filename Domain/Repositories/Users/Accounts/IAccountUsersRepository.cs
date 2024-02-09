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
using Domain.User.Account;

namespace Domain.Repositories.Users.Accounts
{
    public interface IAccountUsersRepository : IRepository<AccountUsers>
    {
        Task<IEnumerable<AccountUsers>> GetUserAccountsByAccountIdAsync(int? groupId);
    }
}
