using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Formacion.Data.Migrations.Scheduler
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShedulerConfigs",
                columns: table => new
                {
                    SchedulerConfigId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Occurs = table.Column<int>(type: "int", nullable: false),
                    NumberOccurs = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShedulerConfigs", x => x.SchedulerConfigId);
                });

            migrationBuilder.CreateTable(
                name: "SchedulerDailyConfigs",
                columns: table => new
                {
                    SchedulerDailyConfigId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Frecuency = table.Column<short>(type: "smallint", nullable: false),
                    OneTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    TypeUnit = table.Column<short>(type: "smallint", nullable: true),
                    NumberOccurs = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    SchedulerConfigId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchedulerDailyConfigs", x => x.SchedulerDailyConfigId);
                    table.ForeignKey(
                        name: "FK_SchedulerDailyConfigs_ShedulerConfigs_SchedulerConfigId",
                        column: x => x.SchedulerConfigId,
                        principalTable: "ShedulerConfigs",
                        principalColumn: "SchedulerConfigId");
                });

            migrationBuilder.CreateTable(
                name: "SchedulerMonthlyConfigs",
                columns: table => new
                {
                    SchedulerMonthlyConfigId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SchedulerConfigId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<short>(type: "smallint", nullable: false),
                    DayMonth = table.Column<short>(type: "smallint", nullable: true),
                    EveryNumberMonths = table.Column<short>(type: "smallint", nullable: false),
                    TypesEvery = table.Column<short>(type: "smallint", nullable: true),
                    TypesDayEvery = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchedulerMonthlyConfigs", x => x.SchedulerMonthlyConfigId);
                    table.ForeignKey(
                        name: "FK_SchedulerMonthlyConfigs_ShedulerConfigs_SchedulerConfigId",
                        column: x => x.SchedulerConfigId,
                        principalTable: "ShedulerConfigs",
                        principalColumn: "SchedulerConfigId");
                });

            migrationBuilder.CreateTable(
                name: "SchedulerWeeklyConfigs",
                columns: table => new
                {
                    SchedulerWeeklyConfigId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SchedulerConfigId = table.Column<int>(type: "int", nullable: false),
                    Every = table.Column<short>(type: "smallint", nullable: false),
                    Monday = table.Column<bool>(type: "bit", nullable: false),
                    Tuesday = table.Column<bool>(type: "bit", nullable: false),
                    Wednesday = table.Column<bool>(type: "bit", nullable: false),
                    Thursday = table.Column<bool>(type: "bit", nullable: false),
                    Friday = table.Column<bool>(type: "bit", nullable: false),
                    Saturday = table.Column<bool>(type: "bit", nullable: false),
                    Sunday = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchedulerWeeklyConfigs", x => x.SchedulerWeeklyConfigId);
                    table.ForeignKey(
                        name: "FK_SchedulerWeeklyConfigs_ShedulerConfigs_SchedulerConfigId",
                        column: x => x.SchedulerConfigId,
                        principalTable: "ShedulerConfigs",
                        principalColumn: "SchedulerConfigId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SchedulerDailyConfigs_SchedulerConfigId",
                table: "SchedulerDailyConfigs",
                column: "SchedulerConfigId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SchedulerMonthlyConfigs_SchedulerConfigId",
                table: "SchedulerMonthlyConfigs",
                column: "SchedulerConfigId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SchedulerWeeklyConfigs_SchedulerConfigId",
                table: "SchedulerWeeklyConfigs",
                column: "SchedulerConfigId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SchedulerDailyConfigs");

            migrationBuilder.DropTable(
                name: "SchedulerMonthlyConfigs");

            migrationBuilder.DropTable(
                name: "SchedulerWeeklyConfigs");

            migrationBuilder.DropTable(
                name: "ShedulerConfigs");
        }
    }
}
