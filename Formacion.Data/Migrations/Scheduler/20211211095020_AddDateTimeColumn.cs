using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Formacion.Data.Migrations.Scheduler
{
    public partial class AddDateTimeColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ShedulerConfigs",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "ShedulerConfigs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShedulerConfigs_Name",
                table: "ShedulerConfigs",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ShedulerConfigs_Name",
                table: "ShedulerConfigs");

            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "ShedulerConfigs");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ShedulerConfigs",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);
        }
    }
}
