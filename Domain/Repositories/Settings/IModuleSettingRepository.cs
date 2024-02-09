using Domain.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.Settings
{
    public interface IModuleSettingRepository : IRepository<ModuleSetting>
    {
        Task<IEnumerable<ModuleSetting>> GetModuleSettingByIdAsync(int Id);
    }
}
