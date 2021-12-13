using Formacion.Data.Models.Scheluder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Formacion.Data.Migrations.Scheduler
{
    public class SchedulerConfigsConfiguration : IEntityTypeConfiguration<SchedulerConfig>
    {
        public void Configure(EntityTypeBuilder<SchedulerConfig> builder)
        {
            builder.ToTable("ShedulerConfigs")
               .HasKey(K => K.SchedulerConfigId);
            builder.Property(P => P.Name)
                .HasMaxLength(255)
                .IsRequired();
            builder.HasIndex(I => I.Name)
                .IsUnique(); 

        }
    }
    
}
