using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.Core.Notifications
{
    public class TransactionMessage : INotification
    {
        public string TransactionID { get; set; } = null!;
        public string TransactionType { get; set; } = null!;
        public string Group { get; set; } = null!;
        public string? TrafficType { get; set; }
        public DateTime ProcessDate { get; } = DateTime.Now;
    }    
}