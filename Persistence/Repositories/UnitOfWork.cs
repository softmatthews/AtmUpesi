using Domain.Enums;
using Domain.Repositories;
using Domain.Repositories.Settings;
using MongoDB.Driver;
using Persistence.MongoDB;
using Persistence.Repositories.ElsaWorkFlow;
using Persistence.Repositories.Settings;
using Persistence.Repositories.Settings.Modules;
using Domain.Repositories.Users.Groups;
using Domain.Repositories.Users;
using Persistence.Repositories.User.Groups;
using Persistence.Repositories.User;
using Elsa.Persistence.EntityFramework.Core;
using MongoDB.Bson;
using Persistence.Repositories.User.Accounts;
using Persistence.Repositories.User.Transactions;
using Domain.Repositories.Users.Accounts;
using Domain.Repositories.Users.Transactions;

namespace Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private readonly IMongoDbSettings _mongoSettings;

#pragma warning disable CS8618 // Instantiates when needed, to increase performance        
        public UnitOfWork(DataContext context, IMongoDbSettings mongoSettings)
#pragma warning restore CS8618
        {
            _mongoSettings = mongoSettings;
            _context = context;
        }

        #region Declarations
        /// <summary>
        /// Transactions
        /// </summary>
       
        private SettingsRepository? settings;        

        // User 
        private GroupRepository group;
        private GroupUsersRepository groupUsers;
        private RightsRepository rights;
        private GroupRightRepository groupRights;
        private GroupATMRepository groupATM;

         private TransactionRepository transaction;
        private AccountTransactionsRepository accountTransactions;
         private AccountRepository account;
        private AccountUsersRepository accountUsers;

       

        // Settings
        private ModuleRepository? modules;
        private ModulesSettingRepository? modulesSettings;
        private LogsRepository? logs;

       
        // Elsa
        #endregion

        // Repositories

      
        #region Settings
        public ISettingsRepository Settings
        {
            get
            {
                settings ??= new SettingsRepository(_context);
                return settings;
            }
            private set
            {
            }
        }
        public IModuleRepository Modules
        {
            get
            {
                modules ??= new ModuleRepository(_context);
                return modules;
            }
            private set
            {
            }
        }
        public IModuleSettingRepository ModulesSetting
        {
            get
            {
                modulesSettings ??= new ModulesSettingRepository(_context);
                return modulesSettings;
            }
            private set
            {
            }
        }
        public ILogsRepository Logs
        {
            get
            {
                logs ??= new LogsRepository(_context);
                return logs;
            }
            private set
            {
            }
        }
        #endregion


        #region User
        public IGroupRepository Group

        {
            get
            {
                group ??= new GroupRepository(_context);
                return group;
            }
            private set
            {
            }
        }
        public IRightRepository Rights
        {
            get
            {
                rights ??= new RightsRepository(_context);
                return rights;
            }
            private set
            {
            }
        }

        public IGroupRightRepository GroupRight
        {
            get
            {
                groupRights ??= new GroupRightRepository(_context);
                return groupRights;
            }
            private set
            {

            }

        }


        
          public IAccountRepository Account
        {
            get
            {
                account ??= new AccountRepository(_context);
                return account;
            }
            private set
            {
            }
        }

  public ITransactionRepository Transaction
        {
            get
            {
                transaction ??= new TransactionRepository(_context);
                return transaction;
            }
            private set
            {
            }
        }
        public IAccountUsersRepository AccountUser
        {
            get
            {
                accountUsers ??= new AccountUsersRepository(_context);
                return accountUsers;
            }
            private set
            {

            }

        }
                

        public IAccountTransactionsRepository AccountTransaction
        {
            get
            {
                accountTransactions ??= new AccountTransactionsRepository(_context);
                return accountTransactions;
            }
            private set
            {

            }

        }


        
        public IGroupATMRepository GroupATM
        {
            get
            {
                groupATM ??= new GroupATMRepository(_context);
                return groupATM;
            }
            private set
            {

            }
        }

        public IGroupUsersRepository GroupUsers
        {
            get
            {
                groupUsers ??= new GroupUsersRepository(_context);
                return groupUsers;
            }
            private set
            {
            }
        }

       

        #endregion



        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}