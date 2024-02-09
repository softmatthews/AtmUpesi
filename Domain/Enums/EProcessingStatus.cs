using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    /// <summary>
    /// Possible result types for all types of processing
    /// </summary>
    public enum EProcessingStatus
    {
        FAILED,
        PENDING,
        REDIRECTED,
        SUCCESS
    }
}
