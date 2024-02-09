using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    /// <summary>
    /// Acceptable module setting types
    /// </summary>
    public enum EModuleSettings
    {
        BOOLEAN,
        NUMBER,
        STRING, 
        CHOICE,
    }

    /// <summary>
    /// Acceptable spooler setting types
    /// </summary>
    public enum ESpoolerSettings
    {
        PRINTER,
        SAVE_DIRECTORY,
        SPOOLER_DIRECTORY,
        PRINTER_DIRECTORY,
        TEMP_DIRECTORY,        
    }

}
