using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.User.Account;

namespace Persistence.Configurations.Users.Accounts
{
    public class AccountUsersConfiguration : IEntityTypeConfiguration<AccountUsers>
    {
        public void Configure(EntityTypeBuilder<AccountUsers> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(d => d.Account)
               .WithMany(s => s.AccountUsers)
               .HasForeignKey(d => d.AccountId)
               .IsRequired(false);
            builder.HasOne(d => d.User)
              .WithMany(s => s.AccountUsers)
              .HasForeignKey(d => d.UserId)
              .IsRequired(false)
              .OnDelete(DeleteBehavior.Cascade);
            builder.Property(s => s.CreatedAt).HasDefaultValueSql("getdate()");

        }

    }
}
