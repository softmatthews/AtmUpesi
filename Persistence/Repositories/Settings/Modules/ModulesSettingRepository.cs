using Domain.Repositories.Settings;
using Domain.Settings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Settings.Modules
{

    public class ModulesSettingRepository : Repository<ModuleSetting>, IModuleSettingRepository
    {
        public ModulesSettingRepository(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ModuleSetting>> GetModuleSettingByIdAsync(int id)
        {
            var module = await _entities
              .Where(v => v.Id == id)
              .ToListAsync();
            //.FirstOrDefaultAsync();
            return module;
        }





    }
}

