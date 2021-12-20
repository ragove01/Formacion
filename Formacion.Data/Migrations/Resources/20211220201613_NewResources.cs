using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

#nullable disable

namespace Formacion.Data.Migrations.Resources
{
    [ExcludeFromCodeCoverage]
    public partial class NewResources : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TextResources",
                columns: new[] { "TextResourceId", "TextIndex", "TextValue" },
                values: new object[,]
                {
                    { 55, "NameConfigNotFound", "No se ha encontrado una configuración con el nombre{0}" },
                    { 56, "NameRequired", "El nombre es obligatorio" },
                    { 57, "NameDuplicate", "Ya existe otra configuración con el mismo nombre" }
                });




            migrationBuilder.InsertData(
                table: "TextResourcesCulture",
                columns: new[] { "TextResourceCultureId", "ResourceCultureId", "TextResourceId", "TextValue" },
                values: new object[,]
                {
                    { 112, 1, 55, "Config with name '{0}' not found" },
                    { 113, 1, 56, "The name is required" },
                    { 114, 1, 57, "There is already configuration with the same name" },
                    { 115, 2, 55, "Config with name '{0}' not found" },
                    { 116, 2, 56, "The name is required" },
                    { 117, 2, 57, "There is already configuration with the same name" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
