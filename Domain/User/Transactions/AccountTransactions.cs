using Microsoft.AspNetCore.Identity;
using Domain.User.Account;

namespace Domain.User.Transactions
{
    /// <summary>
    /// Class to handle Many to Many relationship between Accounts and tranxtion
    /// </summary>
    public class AccountTransactions
    {
        public int Id { get; set; }
        public Transaction Transaction { get; set; } = null!;
        public string TransactionId { get; set; } = null!;
        public Domain.User.Account.Account Account { get; set; } = null!;
        public int AccountId { get; set; }
        public string? LastModifiedDate { get; set; } = new DateTime().ToLongTimeString();
        public DateTime CreatedAt { get; set; } = new DateTime();

    }
}