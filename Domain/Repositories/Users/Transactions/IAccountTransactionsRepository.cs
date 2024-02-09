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
using Domain.User.Transactions;

namespace Domain.Repositories.Users.Transactions
{
    public interface IAccountTransactionsRepository : IRepository<AccountTransactions>
    {
        Task<IEnumerable<AccountTransactions>> GetAccountTransactionsByAccountIdAsync(int? AccountId);
    }
}
