using Domain.User.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.Users.Tokens
{
    public class RefreshTokenFamilyConfiguration : IEntityTypeConfiguration<RefreshTokenFamily>
    {
        public void Configure(EntityTypeBuilder<RefreshTokenFamily> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.Tokens);
        }
    }
}
