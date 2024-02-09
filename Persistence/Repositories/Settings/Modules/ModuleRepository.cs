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
    public class ModuleRepository : Repository<Module>, IModuleRepository
    {
        public ModuleRepository(DbContext context) : base(context)
        {
        }

        public async Task<Module?> GetModuleAsync(string moduleName)
        {
            var module = await _entities
              .Where(v => v.Name == moduleName)
              .FirstOrDefaultAsync();

            return module;
        }
        public async Task<IEnumerable<ModuleSetting>> GetModuleByNameAsync(string moduleName)
        {
            var module = await _entities
              .Where(v => v.Name == moduleName)
             .SelectMany(item => item.ModuleSettings)
              .ToListAsync();

            return module;
        }
        public async Task<IEnumerable<ModuleSetting>> GetModuleSettingByIdAsync(int Id)
        {
            var module = await _entities
             .SelectMany(item => item.ModuleSettings)
             .Where(item => item.Id == Id)
             .ToListAsync();

            return module;
        }

        public async Task<IEnumerable<Module>> GetAllModuleSettingAsync()
        {
            return await _entities
                      .OrderByDescending(d => d.CreatedAt)
                       .ToListAsync();
        }

    }
}

