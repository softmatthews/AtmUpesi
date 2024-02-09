using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.User.Account
{
    /// <summary>
    /// Accounts of users are used to track assignments to ATMs, news and authorization
    /// </summary>
    public class Account
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; } = null!;
        public string Name { get; set; } = null!;
        public double Amount {get; set;}
        public string Currency {get; set;}="KES";
        public bool Status { get; set; } = true;
        public DateTime LastModifiedDate { get; set; } = DateTime.Now;
        public string? LastModifiedBy { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.Now; 
        public ICollection<AccountUsers> AccountUsers { get; set; } = new List<AccountUsers>();
    }

}