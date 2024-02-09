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
    public interface ITransactionRepository : IRepository<Transaction>
    {
        Task<Transaction?> GetTransactionByNameAsync(string transactionName);
        Task<Transaction?> GetTransactionByIdAsync(int? transactionId);
        Task<IEnumerable<Transaction>> GetAllTransactionsAsync();
    }
}
