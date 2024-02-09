using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Notifications
{
    public class SpoolerNotification : INotification
    {
        public string? NotificationType = null!;
        public string TransactionID = null!;
        public string TransactionType = null!;
    }
}
