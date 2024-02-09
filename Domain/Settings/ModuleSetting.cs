using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Settings
{
    /// <summary>
    /// Dynamic module settings to account for any number and any type
    /// </summary>
    public class ModuleSetting
    {
        public int Id { get; set; }
        public int ModuleId { get; set; }
        public Module Module { get; set; } = null!;
        public EModuleSettings SettingType { get; set; }
        public DateTime ModifiedAt { get; set; } = new DateTime();
        public string HelperText { get; set; } = null!;
        public string Key { get; set; } = null!;
        public string? Value { get; set; }
    }

}