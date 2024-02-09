using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    /// <summary>
    /// Application wide acceptable query filter values
    /// </summary>
    public enum EQueryFilters
    {
        LesserThan,
        LesserThanOrEqual,
        GreaterThanOrEqual,
        GreaterThan,
        Contains,
        Exact
    }
}
