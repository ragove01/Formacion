using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Formacion.Data.Migrations.Resources
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ResourceCultures",
                columns: table => new
                {
                    ResourceCultureId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CultureName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceCultures", x => x.ResourceCultureId);
                });

            migrationBuilder.CreateTable(
                name: "TextResources",
                columns: table => new
                {
                    TextResourceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TextIndex = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TextValue = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextResources", x => x.TextResourceId);
                });

            migrationBuilder.CreateTable(
                name: "TextResourcesCulture",
                columns: table => new
                {
                    TextResourceCultureId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TextResourceId = table.Column<int>(type: "int", nullable: false),
                    ResourceCultureId = table.Column<int>(type: "int", nullable: false),
                    TextValue = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextResourcesCulture", x => x.TextResourceCultureId);
                    table.ForeignKey(
                        name: "FK_TextResourcesCulture_ResourceCultures_ResourceCultureId",
                        column: x => x.ResourceCultureId,
                        principalTable: "ResourceCultures",
                        principalColumn: "ResourceCultureId");
                    table.ForeignKey(
                        name: "FK_TextResourcesCulture_TextResources_TextResourceId",
                        column: x => x.TextResourceId,
                        principalTable: "TextResources",
                        principalColumn: "TextResourceId");
                });

            migrationBuilder.InsertData(
                table: "ResourceCultures",
                columns: new[] { "ResourceCultureId", "CultureName" },
                values: new object[,]
                {
                    { 1, "en-GB" },
                    { 2, "en-US" }
                });

            migrationBuilder.InsertData(
                table: "TextResources",
                columns: new[] { "TextResourceId", "TextIndex", "TextValue" },
                values: new object[,]
                {
                    { 1, "ConfigMustHasValue", "Configuración debe tener un valor" },
                    { 2, "CurrentDateInvalid", "La fecha actual es incorrecta" },
                    { 3, "DateTimeMustHasValue", "La fecha debe tener un valor" },
                    { 4, "EndAtMinorStartingAt", "'Finaliza en' debe ser mas grande que 'Comienza en'" },
                    { 5, "EndAtNotHasValue", "'Finaliza en' debe tener un valor" },
                    { 6, "EndDateAerlierCurrentDate", "La fecha final no puede ser menor que la fecha actual" },
                    { 7, "EndDateGreatStartDate", "Fecha de finalización debe ser mayor que la fecha de comienzo" },
                    { 8, "EveryMustGreatZero", "Configuración semanal: 'Cada' debe ser mayor que zero" },
                    { 9, "MonthBeBetween1And12", "Configuracion mensual: mes(s) debe estar entre 1 y 12" },
                    { 10, "MustIndicateDayGreatZero", "Configuracion mensual: debe indicar un día del mes mayor que cero" },
                    { 11, "MustIndicateDayLes31", "Configuracion mensual: debe indicar un día del mes menor o igual que 31" },
                    { 12, "MustIndicateDayOfMonth", "Configuracion mensual: debe indicar un día del mes" },
                    { 13, "MustIndicateTypeEveryDay", "Configuracion mensual: debe indicar el tipo de cada día" },
                    { 14, "MustIndicateTypeOfDayWeek", "Configuracion mensual: debe indicar el tipo de día de la semana" },
                    { 15, "MustSelectDayWeek", "Configuración semanal: debe seleccionar un o mas días de la semana" },
                    { 16, "NotCalculate", "No hay calculo" },
                    { 17, "NotNextExecution", "No hay proxima ejecución" },
                    { 18, "NumberMustGreaZero", "Número de repeticiones debe ser mayor que cero" },
                    { 19, "OccursGreatZero", "'Ocurre cada' debe ser mayo o igual que cero" },
                    { 20, "OnceAtValue", "'Ocurre en' debe tener un valor" },
                    { 21, "StartDateInvalid", "Fecha de comienzo incorrecta" },
                    { 22, "StartingAtNotHasValue", "'Comienza en' debe tener un valor" },
                    { 23, "WrongConfiguration", "Configuración incorrecta" },
                    { 24, "FormatterDailyFrecuency_TextEvery", "cada {0} {1} entre las {2} y las {3}" },
                    { 25, "FormatterDailyFrecuency_TextOnce", "Ocurre una vez en {0}" },
                    { 26, "FormatterMonthly_TextBase", "Ocurre {0} cada {1} meses" },
                    { 27, "FormatterMonthly_TextTypeDay", "el {0}" },
                    { 28, "FormatterMonthly_TextTypeEvery", "el {0} {1}" },
                    { 29, "FormatterOnce_TextBase", "Ocurre una vez. El horario se utilizará el {0} a las {1} comenzando el {2}" },
                    { 30, "FormatterRecurring_TextBase", "{0} empezando el {1}" },
                    { 31, "FormatterRecurring_TextNoConfigWeekly", "Ocurre cada {0}{1}{2}. El horario se utilizará el {3} a las {4}" },
                    { 32, "FormatterWeekly_TextAnd", " y" },
                    { 33, "FormatterWeekly_TextBase", "Ocurre cada {0} semanas en {1}" },
                    { 34, "friday", "viernes" },
                    { 35, "monday", "lunes" },
                    { 36, "saturday", "sabado" },
                    { 37, "sunday", "domingo" },
                    { 38, "thursday", "jueves" },
                    { 39, "tuesday", "martes" },
                    { 40, "TypesEveryDayMonthly_Weekday", "entre semana" }
                });

            migrationBuilder.InsertData(
                table: "TextResources",
                columns: new[] { "TextResourceId", "TextIndex", "TextValue" },
                values: new object[,]
                {
                    { 41, "TypesEveryMonthly_First", "primer" },
                    { 42, "TypesEveryMonthly_Fourth", "cuarto" },
                    { 43, "TypesEveryMonthly_Last", "último" },
                    { 44, "TypesEveryMonthly_Second", "segundo" },
                    { 45, "TypesEveryMonthly_Third", "tercero" },
                    { 46, "TypesEveryDayMonthly_Weekend", "fin de semana" },
                    { 47, "TypesOccurs_Daily", "día" },
                    { 48, "TypesOccurs_Monthly", "mes" },
                    { 49, "TypesOccurs_Weekly", "semana" },
                    { 50, "TypesUnitsDailyFrecuency_Hours", "horas" },
                    { 51, "TypesUnitsDailyFrecuency_Minutes", "minutos" },
                    { 52, "TypesUnitsDailyFrecuency_Seconds", "segundos" },
                    { 53, "wednesday", "miercoles" },
                    { 54, "EnumConversionError", "Error al convertir el valor {0} en {1}" }
                });

            migrationBuilder.InsertData(
                table: "TextResourcesCulture",
                columns: new[] { "TextResourceCultureId", "ResourceCultureId", "TextResourceId", "TextValue" },
                values: new object[,]
                {
                    { 1, 1, 1, "Config must have a value" },
                    { 2, 1, 2, "The current date is invalid" },
                    { 3, 1, 3, "Date Time must have a value" },
                    { 4, 1, 4, "'End at' must be great than 'Starting at'" },
                    { 5, 1, 5, "'End at' must have a value" },
                    { 6, 1, 6, "the end date cannot be earlier than the current date" },
                    { 7, 1, 7, "End date must be great than start date" },
                    { 8, 1, 8, "Weekly configuration: 'Every' must be greater than zero" },
                    { 9, 1, 9, "Montly configuration: month(s) must be between 1 and 12" },
                    { 10, 1, 10, "Montly configuration: must indicate a day of the month great than zero" },
                    { 11, 1, 11, "Montly configuration: must indicate a day of the month less or equal than 31" },
                    { 12, 1, 12, "Montly configuration: must indicate a day of the month" },
                    { 13, 1, 13, "Montly configuration: must indicate the type every day" },
                    { 14, 1, 14, "Montly configuration: must indicate a type of day week" },
                    { 15, 1, 15, "Weekly configuration: must select one or more days of the week" },
                    { 16, 1, 16, "Scheluder not calculate" },
                    { 17, 1, 17, "Not next execution time" },
                    { 18, 1, 18, "Number of occurs must be great than zero" },
                    { 19, 1, 19, "'Occurs every' must be greater or equal than zero" },
                    { 20, 1, 20, "'Once at' must have a value" },
                    { 21, 1, 21, "Start Date is invalid" },
                    { 22, 1, 22, "'Starting at' must have a value" },
                    { 23, 1, 23, "wrong configuration" },
                    { 24, 1, 24, "ever {0} {1} between {2} and {3}" },
                    { 25, 1, 25, "occurs once at {0}" },
                    { 26, 1, 26, "Occurs {0} of very {1} months" },
                    { 27, 1, 27, "the {0}" },
                    { 28, 1, 28, "the {0} {1}" },
                    { 29, 1, 29, "Occurs once. Schedule will be used on {0} at {1} starting on {2}" },
                    { 30, 1, 30, "{0} starting on {1}" },
                    { 31, 1, 31, "Occurs every {0}{1}{2}. Schedule will be used on {3} at {4}" },
                    { 32, 1, 32, " and " },
                    { 33, 1, 33, "Occurs every {0} weeks on {1}" },
                    { 34, 1, 34, "friday" },
                    { 35, 1, 35, "monday" },
                    { 36, 1, 36, "saturday" },
                    { 37, 1, 37, "sunday" },
                    { 38, 1, 38, "thursday" },
                    { 39, 1, 39, "tuesday" },
                    { 40, 1, 40, "weekday" },
                    { 41, 1, 41, "first" },
                    { 42, 1, 42, "fourth" }
                });

            migrationBuilder.InsertData(
                table: "TextResourcesCulture",
                columns: new[] { "TextResourceCultureId", "ResourceCultureId", "TextResourceId", "TextValue" },
                values: new object[,]
                {
                    { 43, 1, 43, "last" },
                    { 44, 1, 44, "second" },
                    { 45, 1, 45, "third" },
                    { 46, 1, 46, "weekend" },
                    { 47, 1, 47, "daily" },
                    { 48, 1, 48, "monthly" },
                    { 49, 1, 49, "weekly" },
                    { 50, 1, 50, "hours" },
                    { 51, 1, 51, "minutes" },
                    { 52, 1, 52, "seconds" },
                    { 53, 1, 53, "wednesday" },
                    { 54, 1, 54, "Error to convert {0} at {1}" },
                    { 55, 2, 1, "Config must have a value" },
                    { 56, 2, 2, "The current date is invalid" },
                    { 57, 2, 3, "Date Time must have a value" },
                    { 58, 2, 4, "'End at' must be great than 'Starting at'" },
                    { 59, 2, 5, "'End at' must have a value" },
                    { 60, 2, 6, "the end date cannot be earlier than the current date" },
                    { 61, 2, 7, "End date must be great than start date" },
                    { 62, 2, 8, "Weekly configuration: 'Every' must be greater than zero" },
                    { 63, 2, 9, "Montly configuration: month(s) must be between 1 and 12" },
                    { 64, 2, 10, "Montly configuration: must indicate a day of the month great than zero" },
                    { 65, 2, 11, "Montly configuration: must indicate a day of the month less or equal than 31" },
                    { 66, 2, 12, "Montly configuration: must indicate a day of the month" },
                    { 67, 2, 13, "Montly configuration: must indicate the type every day" },
                    { 68, 2, 14, "Montly configuration: must indicate a type of day week" },
                    { 69, 2, 15, "Weekly configuration: must select one or more days of the week" },
                    { 70, 2, 16, "Scheluder not calculate" },
                    { 71, 2, 17, "Not next execution time" },
                    { 72, 2, 18, "Number of occurs must be great than zero" },
                    { 73, 2, 19, "'Occurs every' must be greater or equal than zero" },
                    { 74, 2, 20, "'Once at' must have a value" },
                    { 75, 2, 21, "Start Date is invalid" },
                    { 76, 2, 22, "'Starting at' must have a value" },
                    { 77, 2, 23, "wrong configuration" },
                    { 78, 2, 24, "ever {0} {1} between {2} and {3}" },
                    { 79, 2, 25, "occurs once at {0}" },
                    { 80, 2, 26, "Occurs {0} of very {1} months" },
                    { 81, 2, 27, "the {0}" },
                    { 82, 2, 28, "the {0} {1}" },
                    { 83, 2, 29, "Occurs once. Schedule will be used on {0} at {1} starting on {2}" },
                    { 84, 2, 30, "{0} starting on {1}" }
                });

            migrationBuilder.InsertData(
                table: "TextResourcesCulture",
                columns: new[] { "TextResourceCultureId", "ResourceCultureId", "TextResourceId", "TextValue" },
                values: new object[,]
                {
                    { 85, 2, 31, "Occurs every {0}{1}{2}. Schedule will be used on {3} at {4}" },
                    { 86, 2, 32, " and " },
                    { 87, 2, 33, "Occurs every {0} weeks on {1}" },
                    { 88, 2, 34, "friday" },
                    { 89, 2, 35, "monday" },
                    { 90, 2, 36, "saturday" },
                    { 91, 2, 37, "sunday" },
                    { 92, 2, 38, "thursday" },
                    { 93, 2, 39, "tuesday" },
                    { 94, 2, 40, "weekday" },
                    { 95, 2, 41, "first" },
                    { 96, 2, 42, "fourth" },
                    { 97, 2, 43, "last" },
                    { 98, 2, 44, "second" },
                    { 99, 2, 45, "third" },
                    { 100, 2, 46, "weekend" },
                    { 101, 2, 47, "daily" },
                    { 102, 2, 48, "monthly" },
                    { 103, 2, 49, "weekly" },
                    { 104, 2, 50, "hours" },
                    { 105, 2, 51, "minutes" },
                    { 106, 2, 52, "seconds" },
                    { 107, 2, 53, "wednesday" },
                    { 108, 2, 54, "Error to convert {0} at {1}" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TextResourcesCulture_ResourceCultureId",
                table: "TextResourcesCulture",
                column: "ResourceCultureId");

            migrationBuilder.CreateIndex(
                name: "IX_TextResourcesCulture_TextResourceId",
                table: "TextResourcesCulture",
                column: "TextResourceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TextResourcesCulture");

            migrationBuilder.DropTable(
                name: "ResourceCultures");

            migrationBuilder.DropTable(
                name: "TextResources");
        }
    }
}
