using Domain.Repositories.ElsaWorkFlow;
using Domain.Repositories.Settings;
using Domain.Repositories.Users.Groups;
using Domain.Repositories.Users;
using Domain.Repositories.Users.Transactions;
using Domain.Repositories.Users.Accounts;

namespace Domain.Repositories
{

    public interface IUnitOfWork : IDisposable
    {

        ISettingsRepository Settings { get; }

        #region Settings 
        IModuleRepository Modules { get; }
        ILogsRepository Logs { get; }
        IModuleSettingRepository ModulesSetting { get; }
        #endregion
        #region User
        IGroupRepository Group { get; }
        IGroupUsersRepository GroupUsers { get; }
        IGroupRightRepository GroupRight { get; }
        IGroupATMRepository GroupATM { get; }
        IRightRepository Rights { get; }

        IAccountRepository Account { get; }
        #endregion

        ITransactionRepository Transaction { get; }


        Task<int> Complete();
    }
}