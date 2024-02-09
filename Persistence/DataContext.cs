using Domain.Settings;
using Domain.User;
using Domain.User.Account;
using Domain.User.Group;
using Domain.User.Tokens;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext()
        {
        }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        /**
		* Users
		*/
        public DbSet<Group> Group { get; set; } = null!;
        public DbSet<Right> Right { get; set; } = null!;
        public DbSet<GroupRight> GroupRight { get; set; } = null!;
        public DbSet<GroupATM> GroupATM { get; set; } = null!;
        public DbSet<GroupUsers> UserGroups { get; set; } = null!;
        public DbSet<RefreshTokenFamily> RefreshTokenFamilies { get; set; } = null!;
        public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;

        public DbSet<Account> Account { get; set; } = null!;
        public DbSet<AccountUsers> UserAccounts { get; set; } = null!;

        //public DbSet<Module> Module { get; set; } = null!;
        //public DbSet<ModuleSetting> ModuleSetting { get; set; } = null!;
        public DbSet<Log> Logs { get; set; } = null!;
        public DbSet<ATM> BICs { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            // User properties collection
            builder.ApplyConfiguration(new Configurations.Users.RightsConfiguration());
            builder.ApplyConfiguration(new Configurations.Users.Groups.GroupRightsConfiguration());
            builder.ApplyConfiguration(new Configurations.Users.Groups.GroupATMConfiguration());
            builder.ApplyConfiguration(new Configurations.Users.Groups.GroupConfiguration());
            builder.ApplyConfiguration(new Configurations.Users.Groups.GroupUsersConfiguration());
            builder.ApplyConfiguration(new Configurations.Users.Tokens.RefreshTokenConfiguration());
            builder.ApplyConfiguration(new Configurations.Users.Tokens.RefreshTokenFamilyConfiguration());
            
            builder.ApplyConfiguration(new Configurations.Users.Accounts.AccountConfiguration ());            
            builder.ApplyConfiguration(new Configurations.Users.Accounts.AccountUsersConfiguration());


            // Settings properties collection
            //builder.ApplyConfiguration(new Configurations.Settings.ModuleConfiguration());
            //builder.ApplyConfiguration(new Configurations.Settings.ModuleSettingConfiguration());
            builder.ApplyConfiguration(new Configurations.Settings.LogConfiguration());
            builder.ApplyConfiguration(new Configurations.Settings.BICConfiguration());
        }
    }
}