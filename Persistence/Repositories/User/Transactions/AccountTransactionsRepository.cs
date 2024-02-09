using Domain.Repositories;
using Domain.Repositories.Users.Transactions;
using Domain.Settings;
using Domain.User.Transactions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.User.Transactions
{
    public class AccountTransactionsRepository : Repository<AccountTransactions>, IAccountTransactionsRepository
    {
        public AccountTransactionsRepository(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<AccountTransactions>> GetAccountTransactionsByAccountIdAsync(int? AccountId)
        {
            var accountTransactions = await _entities
 .Where(v => v.AccountId == AccountId)
 .ToListAsync();
            return accountTransactions;
        }

      


    }
}