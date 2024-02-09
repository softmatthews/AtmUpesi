using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.User.Transactions;

namespace Persistence.Configurations.Users.Transactions
{
    public class AccountTransactionsConfiguration : IEntityTypeConfiguration<AccountTransactions>
    {
        public void Configure(EntityTypeBuilder<AccountTransactions> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(d => d.Transaction)
               .WithMany(s => s.AccountTransactions)
               .HasForeignKey(d => d.TransactionId)
               .IsRequired(false);
            builder.HasOne(d => d.Account);
            builder.Property(s => s.CreatedAt).HasDefaultValueSql("getdate()");

        }

    }
}
