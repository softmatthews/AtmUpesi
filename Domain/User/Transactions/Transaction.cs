using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.User.Transactions
{
    /// <summary>
    /// </summary>
    public class Transaction
    {
        public int Id { get; set; }
        public double Amount {get; set;}
        public string Currency {get; set;}="KES";
         public string TransactionType { get; set; } = null!; //Withdrawal, Deposit
          public string Details{ get; set; } = null!;
        public bool Status { get; set; } = true;
        public DateTime LastModifiedDate { get; set; } = DateTime.Now;
        public string? LastModifiedBy { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.Now; 
         public ICollection<AccountTransactions> AccountTransactions { get; set; } = new List<AccountTransactions>();
    }

}