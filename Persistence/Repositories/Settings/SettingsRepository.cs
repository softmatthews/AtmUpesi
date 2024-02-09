using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Repositories.Settings;
using Domain.Settings;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace Persistence.Repositories.Settings
{
    public class SettingsRepository : Repository<ModuleSetting>, ISettingsRepository
    {
        public SettingsRepository(DbContext context) : base(context)
        {
        }

        public async Task<bool> GetIfValidateIncomingEnvelopeAsync()
        {
            var setting = await _entities
                    .Where(v => v.Key == "Validate Incoming Envelope")
                    .FirstOrDefaultAsync();

            bool validateIncomingEnvelope = false;

            if (setting == null) return false;
            if (bool.TryParse(setting.Value, out validateIncomingEnvelope))
            {
                return validateIncomingEnvelope;
            }
            else
            {
                return false;
            }
        }


        public async Task<string?> GetTransactionsSourceFolderAsync()
        {
            var setting = await _entities
                    .Where(v => v.Key == "SRC FOLDER")
                    .FirstOrDefaultAsync();

            string? emailservicetype = setting?.Value;
            return emailservicetype;
        }
        public async Task<string?> GetTransactionsBackupFolderAsync()
        {
            var setting = await _entities
                             .Where(v => v.Key == "BACKUP FOLDER")
                             .FirstOrDefaultAsync();

            string? backupFolder = setting?.Value;
            return backupFolder;
        }

        public async Task<string?> GetTransactionsFailedFolderAsync()
        {
            var setting = await _entities
                             .Where(v => v.Key == "FAILED TRANSACTIONS FOLDER")
                             .FirstOrDefaultAsync();

            string? failedFolder = setting?.Value;
            return failedFolder;
        }

        public async Task<string?> GetEmailServiceTypeAsync()
        {
            var setting = await _entities
                    .Where(v => v.Key == "EMAIL SERVICE TYPE")
                    .FirstOrDefaultAsync();

            string? emailservicetype = setting?.Value;
            return emailservicetype;
        }
        public async Task<string?> GetSmtpPasswordAsync()
        {
            var setting = await _entities
                    .Where(v => v.Key == "SMTP PASSWORD")
                    .FirstOrDefaultAsync();

            string? password = setting?.Value;
            return password;
        }

        public async Task<string?> GetSmtpDomainNameAsync()
        {
            var setting = await _entities
                    .Where(v => v.Key == "SMTP DOMAIN")
                    .FirstOrDefaultAsync();

            string? smtpDomain = setting?.Value;
            return smtpDomain;
        }

        public async Task<int> GetSmtpPortNumberAsync()
        {
            var setting = await _entities
                    .Where(v => v.Key == "SMTP PORT")
                    .FirstOrDefaultAsync();
            if (setting != null && setting?.Value != null)
            {
                bool isNumeric = int.TryParse(setting.Value, out int portnumber);
                if (isNumeric)
                {
                    return portnumber;
                }
            }
            return -1;
        }
        public async Task<string?> GetSmtpUserNameAsync()
        {
            var setting = await _entities
                    .Where(v => v.Key == "SMTP USERNAME")
                    .FirstOrDefaultAsync();

            string? userName = setting?.Value;
            return userName;
        }

        public async Task<string?> GetFromEmailAddressAsync()
        {
            var setting = await _entities
                    .Where(v => v.Key == "SENDER EMAIL")
                    .FirstOrDefaultAsync();

            string? fromEmailAddress = setting?.Value;
            return fromEmailAddress;
        }
        public async Task<string?> GetEmailAddressFromNameAsync()
        {
            var setting = await _entities
                    .Where(v => v.Key == "EMAILER SENDER NAME")
                    .FirstOrDefaultAsync();

            string? emailSenderName = setting?.Value;
            return emailSenderName;
        }
        public async Task<string?> GetDomainNameAsync()
        {
            var setting = await _entities
                    .Where(v => v.Key == "DOMAIN NAME")
                    .FirstOrDefaultAsync();

            string? domainName = setting?.Value;

            if (setting == null) return "";

            return domainName;
        }

        public async Task<int> GetEmailPortNumberAsync()
        {
            var setting = await _entities
                    .Where(v => v.Key == "PORT NUMBER")
                    .FirstOrDefaultAsync();

            if (setting == null || !int.TryParse(setting?.Value, out int portNumber)) return -1;

            return portNumber;
        }

        public async Task<string?> GetLicensedATMFIAsync()
        {
            var setting = await _entities
                    .Where(v => v.Key == "LICENSED ATMFI")
                    .FirstOrDefaultAsync();

            string? licensedBicfi = setting?.Value;

            if (setting == null) return "";

            return licensedBicfi;
        }


        public async Task<IEnumerable<ModuleSetting?>> GetModuleSettingAsync(Module module)
        {
            var setting = await _entities
                .Where(v => v.ModuleId == module.Id)
                .ToListAsync();
            return setting;
        }

        public async Task<int> GetSessionTimeoutAsync()
        {
            var setting = await _entities
                        .Where(v => v.Key == "SESSION TIMEOUT")
                        .FirstOrDefaultAsync();

            if (setting == null || !int.TryParse(setting?.Value, out int timeout)) return 20;

            return timeout;
        }
    }
}
