using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.User;

namespace Persistence.Configurations.Users
{
    public class RightsConfiguration : IEntityTypeConfiguration<Right>
    {
        public void Configure(EntityTypeBuilder<Right> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(s => s.Name).IsRequired();
            builder.Property(s => s.Type).IsRequired();
            builder.Property(s => s.PackageName);
            builder.HasIndex(vf => new { vf.Name, vf.PackageName, vf.Feature }).IsUnique();
            builder.Property(s => s.Feature);
            builder.Property(s => s.Description);
            builder.HasMany(s => s.GroupRights);
            builder.Property(s => s.CreatedAt).HasDefaultValueSql("getdate()");
        }

    }
}
