using Domain.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.Settings
{
	internal class BICConfiguration : IEntityTypeConfiguration<ATM>
	{
		public void Configure(EntityTypeBuilder<ATM> builder)
		{
			builder.HasKey(x => x.Id);
			builder.HasMany(x => x.Groups);
		}
	}
}
