using Formacion.Data.Models.Scheluder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formacion.Data.Migrations.Scheduler
{
    public class SchedulerDailyConfigsConfiguration : IEntityTypeConfiguration<SchedulerDailyConfig>
    {
        public void Configure(EntityTypeBuilder<SchedulerDailyConfig> builder)
        {
            builder.ToTable("SchedulerDailyConfigs")
                .HasKey(D => D.SchedulerDailyConfigId);
            builder.HasOne(C => C.Config)
                    .WithOne(D => D.DailyConfig)
                    .HasForeignKey<SchedulerDailyConfig>(D => D.SchedulerConfigId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired(false)
                    .HasPrincipalKey<SchedulerConfig>(C => C.SchedulerConfigId);


        }
    }
}
