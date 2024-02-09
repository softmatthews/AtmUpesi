using Domain.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.Settings
{
    public interface ISettingsRepository : IRepository<ModuleSetting>
    {

        Task<string?> GetLicensedATMFIAsync();

        Task<bool> GetIfValidateIncomingEnvelopeAsync();
        Task<string?> GetTransactionsSourceFolderAsync();
        Task<string?> GetTransactionsFailedFolderAsync();
        Task<string?> GetTransactionsBackupFolderAsync();

        Task<string?> GetEmailServiceTypeAsync();
        Task<string?> GetFromEmailAddressAsync();
        Task<string?> GetEmailAddressFromNameAsync();
        Task<string?> GetDomainNameAsync();
        Task<int> GetEmailPortNumberAsync();

        Task<string?> GetSmtpDomainNameAsync();
        Task<int> GetSmtpPortNumberAsync();
        Task<string?> GetSmtpUserNameAsync();
        Task<string?> GetSmtpPasswordAsync();

        Task<IEnumerable<ModuleSetting?>> GetModuleSettingAsync(Module module);
        Task<int> GetSessionTimeoutAsync();
    }
}
