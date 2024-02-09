using Domain.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Repositories.Settings
{
    public interface IModuleRepository : IRepository<Module>
    {
        Task<IEnumerable<ModuleSetting>> GetModuleByNameAsync(string moduleName);
        Task<IEnumerable<ModuleSetting>> GetModuleSettingByIdAsync(int Id);
        Task<Module?> GetModuleAsync(string moduleName);
        Task<IEnumerable<Module>> GetAllModuleSettingAsync();
    }
}
