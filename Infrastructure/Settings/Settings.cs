using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Core.Exceptions;
using Application.Interfaces.Security;
using Application.Interfaces.Settings;
using Domain.Repositories;
using Domain.Settings;

namespace Infrastructure.Settings
{
    public class Settings : ISettings
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILicensingService _licensingService;

        public Settings(IUnitOfWork unitOfWork, ILicensingService licensingService)
        {
            this._unitOfWork = unitOfWork;
            this._licensingService = licensingService;
        }

        public async Task<bool> ValidateIncomingEnvelopeAsync()
        {
            return await _unitOfWork.Settings.GetIfValidateIncomingEnvelopeAsync();
        }
        public async Task<IEnumerable<ModuleSetting?>> ModuleSettings(Module module)
        {
            return await _unitOfWork.Settings.GetModuleSettingAsync(module);
        }
        public async Task<string> GetEmailPortNumberAsync()
        {
            var port = await _unitOfWork.Settings.GetEmailPortNumberAsync();
            return port.ToString();
        }

        public async Task<string> GetEmailServiceTypeAsync()
        {
            var emailServiceType = await _unitOfWork.Settings.GetEmailServiceTypeAsync() ?? throw new NotFoundException("Email Service Type not found");
            return emailServiceType;
        }
        public async Task<string> GetTransactionsSourceFolderAsync()
        {
            var sourceFolder = await _unitOfWork.Settings.GetTransactionsSourceFolderAsync() ?? throw new NotFoundException("Transactions source folder not found");
            return sourceFolder;
        }
        public async Task<string> GetTransactionsBackupFolderAsync()
        {
            var backupFolder = await _unitOfWork.Settings.GetTransactionsBackupFolderAsync() ?? throw new NotFoundException("Transactions backup folder not found");
            return backupFolder;
        }
        public async Task<string> GetTransactionsFailedFolderAsync()
        {
            var failedFolder = await _unitOfWork.Settings.GetTransactionsFailedFolderAsync() ?? throw new NotFoundException("Transactions failed folder not found");
            return failedFolder;
        }
        public async Task<string> GetSmtpDomainNameAsync()
        {
            var smtpDomainName = await _unitOfWork.Settings.GetSmtpDomainNameAsync() ?? throw new NotFoundException("SMTP Domain Name not found");
            return smtpDomainName;
        }
        public async Task<int> GetSmtpPortNumberAsync()
        {
            var smtpPortNumber = await _unitOfWork.Settings.GetSmtpPortNumberAsync();
            if (smtpPortNumber == -1)
                throw new NotFoundException("SMTP Port Number not found");
            return smtpPortNumber;
        }
        public async Task<string> GetSmtpUserNameAsync()
        {
            var smtpUserName = await _unitOfWork.Settings.GetSmtpUserNameAsync() ?? throw new NotFoundException("SMTP User Name not found");
            return smtpUserName;
        }
        public async Task<string> GetSmtpPasswordAsync()
        {
            var smtpPassword = await _unitOfWork.Settings.GetSmtpPasswordAsync() ?? throw new NotFoundException("SMTP Password not found");
            return smtpPassword;
        }

        public async Task<string> GetFromEmailAddressAsync()
        {
            var fromEmailAddress = await _unitOfWork.Settings.GetFromEmailAddressAsync() ?? throw new NotFoundException("From Email Address not found");
            return fromEmailAddress;
        }
        public async Task<string> GetEmailAddressFromNameAsync()
        {
            var emailAddressFromName = await _unitOfWork.Settings.GetEmailAddressFromNameAsync() ?? throw new NotFoundException("Email Address From Name not found");
            return emailAddressFromName;
        }
        public async Task<string> GetDomainNameAsync()
        {
            var domainName = await _unitOfWork.Settings.GetDomainNameAsync() ?? throw new NotFoundException("Domain Name not found");
            return domainName;
        }

        public bool CheckIfLicensedATMFI(string atm)
        {
            var licensed = _licensingService.IsValidATM(atm);
            return licensed;
        }

        public async Task<string> GetLicensedATMFIAsync()
        {
            var licensedATMFI = await _unitOfWork.Settings.GetLicensedATMFIAsync() ?? throw new NotFoundException("Licensed ATMFI not found");
            return licensedATMFI;
        }

        public List<string> GetLicensedMXFeatures()
        {
            return new() { "PACS002", "PACS004", "PACS008", "PACS009" };
        }

        public async Task<int> GetSessionTimeoutAsync()
        {
            return 50; //await _unitOfWork.Settings.GetSessionTimeoutAsync();
        }

        public string GetPDFDesignPath()
        {
            string path;
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
            {
                path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"Design", @"Assets", @"Style.css"));
            }
            else
            {
                path = Path.Combine(@"../Infrastructure/Design", @"Assets", "Style.css");
            }
            return path;
        }
    }
}
