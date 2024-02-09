using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.User.Group;

namespace Persistence.Configurations.Users.Groups
{
    public class GroupUsersConfiguration : IEntityTypeConfiguration<GroupUsers>
    {
        public void Configure(EntityTypeBuilder<GroupUsers> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(d => d.Group)
               .WithMany(s => s.GroupUsers)
               .HasForeignKey(d => d.GroupId)
               .IsRequired(false);
            builder.HasOne(d => d.User)
              .WithMany(s => s.GroupUsers)
              .HasForeignKey(d => d.UserId)
              .IsRequired(false)
              .OnDelete(DeleteBehavior.Cascade);
            builder.Property(s => s.CreatedAt).HasDefaultValueSql("getdate()");

        }

    }
}
