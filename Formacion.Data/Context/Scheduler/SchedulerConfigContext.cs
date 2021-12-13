using Formacion.Data.Migrations.Scheduler;
using Formacion.Data.Models.Scheluder;
using Microsoft.EntityFrameworkCore;


namespace Formacion.Data.Context.Scheduler
{
    public class SchedulerConfigContext: DbContext
    {
        public DbSet<SchedulerConfig> Configs { get; set; }
        public DbSet<SchedulerDailyConfig> DailyConfigs { get; set; }
        public DbSet<SchedulerWeeklyConfig> WeeklyConfigs { get; set; }
        public DbSet<SchedulerMonthlyConfig> MonthlyConfigs { get; set; }

        public SchedulerConfigContext() : base()//("Formacion")
        {
           
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
      => options.UseSqlServer($@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Formacion;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<SchedulerConfig>(new SchedulerConfigsConfiguration());
            modelBuilder.ApplyConfiguration<SchedulerDailyConfig>(new SchedulerDailyConfigsConfiguration());
            modelBuilder.ApplyConfiguration<SchedulerMonthlyConfig>(new SchedulerMonthlyConfigsConfiguration());
            modelBuilder.ApplyConfiguration<SchedulerWeeklyConfig>(new SchedulerWeeklyConfigsConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
