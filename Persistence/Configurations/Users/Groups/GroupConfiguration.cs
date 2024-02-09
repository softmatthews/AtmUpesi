using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.User.Group;

namespace Persistence.Configurations.Users.Groups
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(s => s.GroupUsers);
            builder.Property(s => s.Name).IsRequired();
            builder.Property(s => s.Status).IsRequired();
            builder.Property(s => s.CreatedAt).HasDefaultValueSql("getdate()");
        }

    }
}
