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
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(DbContext context) : base(context)
        {
        }

        public async Task<Transaction?> GetTransactionByNameAsync(string transactionName)
        {
            var transaction = await _entities
              .Where(v =>v.TransactionType == transactionName)
              .FirstOrDefaultAsync();

            return transaction;
        }
        public async Task<Transaction?> GetTransactionByIdAsync(int? transactionId)
        {
            var transaction = await _entities
              .Where(v => v.Id == transactionId)
              .FirstOrDefaultAsync();

            return transaction;
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
        {
            return await _entities
                .Include(x => x.AccountTransactions)
                      .OrderByDescending(d => d.AccountTransactions.Count())
                       .ToListAsync();
        }
        
    }
}