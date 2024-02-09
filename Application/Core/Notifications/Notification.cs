using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Notifications
{
    public class Notification : INotification
    {        
        public int Id { get; set; }       
        public string? RelatedTransactionType { get; set; } = null!;
        public string? RelatedTransactionId { get; set; } = null!;
        public string? Details {get; set; } =null!;
        public DateTime? CreatedAt { get; set; } = new DateTime();  
        public string? NotificationColor = null!;
        public string? NotifiedBy = null!;
        //public NotificationRule NotificationRule { get; set; } = null!;
    }

}