using Domain.User.Group;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.Users.Groups
{
    public class GroupATMConfiguration : IEntityTypeConfiguration<GroupATM>
    {
        public void Configure(EntityTypeBuilder<GroupATM> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(vf => new { vf.ATMId, vf.GroupId }).IsUnique();
            builder.HasOne(d => d.ATM)
                .WithMany(s => s.Groups)
                .HasForeignKey(d => d.ATMId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);
            builder.HasOne(d => d.Group)
                .WithMany(s => s.GroupATMs)
                .HasForeignKey(d => d.GroupId)
                .IsRequired(false);
            builder.Property(s => s.CreatedAt).HasDefaultValueSql("getdate()");
        }

    }
}
