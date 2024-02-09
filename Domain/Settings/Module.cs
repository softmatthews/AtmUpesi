using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Settings
{
    /// <summary>
    /// Simple Module declaration for usage within licensing
    /// NB: Will be tightly coupled to licensing
    /// </summary>
    public class Module
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = new DateTime();
        public ICollection<ModuleSetting> ModuleSettings { get; set; } = new List<ModuleSetting>();
    }
}
