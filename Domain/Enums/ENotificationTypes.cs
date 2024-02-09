using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    /// <summary>
    /// NOTIFICATION TYPES CATEGORIZATIONS
    /// </summary>
    public enum ENotificationTypes
    {
        PDE_OR_PDM,
        Duplicate_Transaction,
        CUSTOM_TRANSACTION_NOTIFICATIONS,
        TRANSACTION,
        NOT_ACKNOWLEDGED_TRANSACTION
    }

}
