﻿// <auto-generated />
using System;
using Formacion.Data.Context.Scheduler;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Formacion.Data.Migrations.Scheduler
{
    [DbContext(typeof(SchedulerConfigContext))]
    partial class SchedulerConfigContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Formacion.Data.Models.Scheluder.SchedulerConfig", b =>
                {
                    b.Property<int>("SchedulerConfigId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SchedulerConfigId"), 1L, 1);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("NumberOccurs")
                        .HasColumnType("int");

                    b.Property<int>("Occurs")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("SchedulerConfigId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("ShedulerConfigs", (string)null);
                });

            modelBuilder.Entity("Formacion.Data.Models.Scheluder.SchedulerDailyConfig", b =>
                {
                    b.Property<int>("SchedulerDailyConfigId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SchedulerDailyConfigId"), 1L, 1);

                    b.Property<TimeSpan?>("EndTime")
                        .HasColumnType("time");

                    b.Property<short>("Frecuency")
                        .HasColumnType("smallint");

                    b.Property<int>("NumberOccurs")
                        .HasColumnType("int");

                    b.Property<TimeSpan?>("OneTime")
                        .HasColumnType("time");

                    b.Property<int>("SchedulerConfigId")
                        .HasColumnType("int");

                    b.Property<TimeSpan?>("StartTime")
                        .HasColumnType("time");

                    b.Property<short?>("TypeUnit")
                        .HasColumnType("smallint");

                    b.HasKey("SchedulerDailyConfigId");

                    b.HasIndex("SchedulerConfigId")
                        .IsUnique();

                    b.ToTable("SchedulerDailyConfigs", (string)null);
                });

            modelBuilder.Entity("Formacion.Data.Models.Scheluder.SchedulerMonthlyConfig", b =>
                {
                    b.Property<int>("SchedulerMonthlyConfigId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SchedulerMonthlyConfigId"), 1L, 1);

                    b.Property<short?>("DayMonth")
                        .HasColumnType("smallint");

                    b.Property<short>("EveryNumberMonths")
                        .HasColumnType("smallint");

                    b.Property<int>("SchedulerConfigId")
                        .HasColumnType("int");

                    b.Property<short>("Type")
                        .HasColumnType("smallint");

                    b.Property<short?>("TypesDayEvery")
                        .HasColumnType("smallint");

                    b.Property<short?>("TypesEvery")
                        .HasColumnType("smallint");

                    b.HasKey("SchedulerMonthlyConfigId");

                    b.HasIndex("SchedulerConfigId")
                        .IsUnique();

                    b.ToTable("SchedulerMonthlyConfigs", (string)null);
                });

            modelBuilder.Entity("Formacion.Data.Models.Scheluder.SchedulerWeeklyConfig", b =>
                {
                    b.Property<int>("SchedulerWeeklyConfigId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SchedulerWeeklyConfigId"), 1L, 1);

                    b.Property<short>("Every")
                        .HasColumnType("smallint");

                    b.Property<bool>("Friday")
                        .HasColumnType("bit");

                    b.Property<bool>("Monday")
                        .HasColumnType("bit");

                    b.Property<bool>("Saturday")
                        .HasColumnType("bit");

                    b.Property<int>("SchedulerConfigId")
                        .HasColumnType("int");

                    b.Property<bool>("Sunday")
                        .HasColumnType("bit");

                    b.Property<bool>("Thursday")
                        .HasColumnType("bit");

                    b.Property<bool>("Tuesday")
                        .HasColumnType("bit");

                    b.Property<bool>("Wednesday")
                        .HasColumnType("bit");

                    b.HasKey("SchedulerWeeklyConfigId");

                    b.HasIndex("SchedulerConfigId")
                        .IsUnique();

                    b.ToTable("SchedulerWeeklyConfigs", (string)null);
                });

            modelBuilder.Entity("Formacion.Data.Models.Scheluder.SchedulerDailyConfig", b =>
                {
                    b.HasOne("Formacion.Data.Models.Scheluder.SchedulerConfig", "Config")
                        .WithOne("DailyConfig")
                        .HasForeignKey("Formacion.Data.Models.Scheluder.SchedulerDailyConfig", "SchedulerConfigId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Config");
                });

            modelBuilder.Entity("Formacion.Data.Models.Scheluder.SchedulerMonthlyConfig", b =>
                {
                    b.HasOne("Formacion.Data.Models.Scheluder.SchedulerConfig", "Config")
                        .WithOne("MonthlyConfig")
                        .HasForeignKey("Formacion.Data.Models.Scheluder.SchedulerMonthlyConfig", "SchedulerConfigId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Config");
                });

            modelBuilder.Entity("Formacion.Data.Models.Scheluder.SchedulerWeeklyConfig", b =>
                {
                    b.HasOne("Formacion.Data.Models.Scheluder.SchedulerConfig", "Config")
                        .WithOne("WeeklyConfig")
                        .HasForeignKey("Formacion.Data.Models.Scheluder.SchedulerWeeklyConfig", "SchedulerConfigId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Config");
                });

            modelBuilder.Entity("Formacion.Data.Models.Scheluder.SchedulerConfig", b =>
                {
                    b.Navigation("DailyConfig");

                    b.Navigation("MonthlyConfig");

                    b.Navigation("WeeklyConfig");
                });
#pragma warning restore 612, 618
        }
    }
}
