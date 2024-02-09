using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.User.Group;

namespace Persistence.Configurations.Users.Groups
{
    public class GroupRightsConfiguration : IEntityTypeConfiguration<GroupRight>
    {
        public void Configure(EntityTypeBuilder<GroupRight> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(vf => new { vf.RightId, vf.GroupId }).IsUnique();
            builder.HasOne(d => d.Right)
              .WithMany(s => s.GroupRights)
              .HasForeignKey(d => d.RightId)
               .OnDelete(DeleteBehavior.Cascade)
              .IsRequired(false);
            builder.HasOne(d => d.Group)
              .WithMany(s => s.GroupClaims)
              .HasForeignKey(d => d.GroupId)
              .IsRequired(false);

            builder.Property(s => s.CreatedAt).HasDefaultValueSql("getdate()");
        }

    }
}
