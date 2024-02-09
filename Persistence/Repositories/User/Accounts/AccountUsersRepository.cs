using Domain.Repositories;
using Domain.Repositories.Users.Accounts;
using Domain.Settings;
using Domain.User.Account;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.User.Accounts
{
    public class AccountUsersRepository : Repository<AccountUsers>, IAccountUsersRepository
    {
        public AccountUsersRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<AccountUsers>> GetUserAccountsByAccountIdAsync(int? accountId)
        {
            var userAccount = await _entities
             .Where(v => v.AccountId == accountId)
             .ToListAsync();
            return userAccount;
        }



    }
}