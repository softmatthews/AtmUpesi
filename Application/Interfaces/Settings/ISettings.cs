using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Settings
{
    public interface ISettings
    {
        Task<bool> ValidateIncomingEnvelopeAsync();
        Task<string> GetEmailServiceTypeAsync();
        Task<string> GetTransactionsSourceFolderAsync();
        Task<string> GetTransactionsBackupFolderAsync();
        Task<string> GetTransactionsFailedFolderAsync();
        Task<string> GetDomainNameAsync();
        Task<string> GetEmailPortNumberAsync();
        Task<string> GetSmtpDomainNameAsync();
        Task<int> GetSmtpPortNumberAsync();
        Task<string> GetSmtpUserNameAsync();
        Task<string> GetSmtpPasswordAsync();
        Task<string> GetFromEmailAddressAsync();
        Task<string> GetEmailAddressFromNameAsync();
        Task<int> GetSessionTimeoutAsync();
        Task<string> GetLicensedATMFIAsync();
        bool CheckIfLicensedATMFI(string bic);
        List<string> GetLicensedMXFeatures();
        string GetPDFDesignPath();
    }
}
