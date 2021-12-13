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
    public class SchedulerMonthlyConfigsConfiguration : IEntityTypeConfiguration<SchedulerMonthlyConfig>
    {
        public void Configure(EntityTypeBuilder<SchedulerMonthlyConfig> builder)
        {
            builder.ToTable("SchedulerMonthlyConfigs")
               .HasKey(D => D.SchedulerMonthlyConfigId);
            builder.HasOne(C => C.Config)
               .WithOne(M => M.MonthlyConfig)
               .HasForeignKey<SchedulerMonthlyConfig>(M => M.SchedulerConfigId)
               .OnDelete(DeleteBehavior.NoAction)
               .IsRequired(false)
               .HasPrincipalKey<SchedulerConfig>(C => C.SchedulerConfigId) ;


        }
    }
}
