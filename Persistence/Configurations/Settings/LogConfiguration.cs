using Domain.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.Settings
{
    public class LogConfiguration: IEntityTypeConfiguration<Log>
    {
		public void Configure(EntityTypeBuilder<Log> builder)
		{
			builder.HasKey(x => x.Id);

			// Analyse Read vs Write

			// builder.HasIndex(x => x.Actor);
			// builder.HasIndex(x => x.Package);
			// builder.HasIndex(x => x.Feature);
			// builder.HasIndex(x => x.Subfeature);
		}
	}
}