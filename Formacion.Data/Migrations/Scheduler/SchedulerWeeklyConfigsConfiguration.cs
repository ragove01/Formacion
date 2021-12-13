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
    public class SchedulerWeeklyConfigsConfiguration : IEntityTypeConfiguration<SchedulerWeeklyConfig>
    {
        public void Configure(EntityTypeBuilder<SchedulerWeeklyConfig> builder)
        {
            builder.ToTable("SchedulerWeeklyConfigs")
               .HasKey(D => D.SchedulerWeeklyConfigId);

            builder.HasOne(C => C.Config)
               .WithOne(M => M.WeeklyConfig)
               .HasForeignKey<SchedulerWeeklyConfig>(W => W.SchedulerConfigId)
               .OnDelete(DeleteBehavior.NoAction);

        }
    }
    
}
