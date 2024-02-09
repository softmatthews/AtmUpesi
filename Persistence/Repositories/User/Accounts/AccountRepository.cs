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
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public AccountRepository(DbContext context) : base(context)
        {
        }

        public async Task<Account?> GetAccountByNameAsync(string accountName)
        {
            var account = await _entities
              .Where(v => v.Name == accountName)
              .FirstOrDefaultAsync();

            return account;
        }
        public async Task<Account?> GetAccountByIdAsync(int? accountId)
        {
            var account = await _entities
              .Where(v => v.Id == accountId)
              .FirstOrDefaultAsync();

            return account;
        }

        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            return await _entities
                .Include(x => x.AccountUsers)
                      //.Include(x => x.AccountNewss)
                      .OrderByDescending(d => d.AccountUsers.Count())
                       .ToListAsync();
        }

    }
}