using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.User.Account;

namespace Persistence.Configurations.Users.Accounts
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(s => s.AccountUsers);
            builder.Property(s => s.Name).IsRequired();
            builder.Property(s => s.Status).IsRequired();
            builder.Property(s => s.CreatedAt).HasDefaultValueSql("getdate()");
        }

    }
}
