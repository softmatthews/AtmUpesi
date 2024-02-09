using Domain.User.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.Users.Tokens
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.RefreshTokenFamily)
                            .WithMany(s => s.Tokens)
                            .HasForeignKey(x => x.RefreshTokenFamilyId);
        }
    }
}
