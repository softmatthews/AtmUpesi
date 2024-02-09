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
    public interface IAccountRepository : IRepository<Account>
    {
        Task<Account?> GetAccountByNameAsync(string accountName);
        Task<Account?> GetAccountByIdAsync(int? accountId);
        Task<IEnumerable<Account>> GetAllAccountsAsync();
    }
}
