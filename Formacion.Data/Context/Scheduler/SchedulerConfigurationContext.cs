using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Formacion.Data.Models.Scheduler;

namespace Formacion.Data.Context.Scheduler
{
    public partial class SchedulerConfigurationContext : DbContext
    {
        public SchedulerConfigurationContext():base(DataBaseConections<SchedulerConfigurationContext>.ContextOptions)
        {
        }

        public SchedulerConfigurationContext(DbContextOptions<SchedulerConfigurationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<SchedulerConfiguration> SchedulerConfigurations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Formacion;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SchedulerConfiguration>(entity =>
            {
                entity.HasKey(e => e.SchedulerId)
                    .HasName("PK__Schedule__8922A209003E54E2");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
