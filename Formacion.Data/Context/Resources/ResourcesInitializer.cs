using Formacion.Data.Context.Resources;
using Formacion.Data.Models.Resources;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Formacion.Data.Context.Resources

{
    public class ResourcesInitializer
    {
      
        public static void Initialize(ModelBuilder builder)
        {
            var listTextResources = ResourcesInitializer.CreateResources();
            builder.Entity<TextResource>().HasData(listTextResources);
            var listCultures = ResourcesInitializer.CreateCultures();
            builder.Entity<ResourceCulture>().HasData(listCultures);
            List<TextResourceCulture> listTextResourcesCultures = new List<TextResourceCulture>();
            int initialResourceId = 1;
             
            listCultures.ForEach(C =>
            {
                listTextResourcesCultures.AddRange(ResourcesInitializer.CreateEnglishTexts(initialResourceId,listTextResources, C));
                initialResourceId += listTextResources.Count;
            });
            builder.Entity<TextResourceCulture>().HasData(listTextResourcesCultures);

        }


        private static List<TextResource> CreateResources()
        {
            int initialId = 1;
            return new List<TextResource>()
            {
                new TextResource{TextResourceId = initialId++,TextIndex = "ConfigMustHasValue",TextValue= "Configuración debe tener un valor"},
                new TextResource{TextResourceId = initialId++,TextIndex = "CurrentDateInvalid",TextValue="La fecha actual es incorrecta"},
                new TextResource{TextResourceId = initialId++,TextIndex = "DateTimeMustHasValue",TextValue="La fecha debe tener un valor"},
                new TextResource{TextResourceId = initialId++,TextIndex = "EndAtMinorStartingAt",TextValue="'Finaliza en' debe ser mas grande que 'Comienza en'"},
                new TextResource{TextResourceId = initialId++,TextIndex = "EndAtNotHasValue",TextValue="'Finaliza en' debe tener un valor"},
                new TextResource{TextResourceId = initialId++,TextIndex = "EndDateAerlierCurrentDate",TextValue="La fecha final no puede ser menor que la fecha actual"},
                new TextResource{TextResourceId = initialId++,TextIndex = "EndDateGreatStartDate",TextValue="Fecha de finalización debe ser mayor que la fecha de comienzo"},
                new TextResource{TextResourceId = initialId++,TextIndex = "EveryMustGreatZero",TextValue="Configuración semanal: 'Cada' debe ser mayor que zero"},
                new TextResource{TextResourceId = initialId++,TextIndex = "MonthBeBetween1And12",TextValue="Configuracion mensual: mes(s) debe estar entre 1 y 12"},
                new TextResource{TextResourceId = initialId++,TextIndex = "MustIndicateDayGreatZero",TextValue="Configuracion mensual: debe indicar un día del mes mayor que cero"},
                new TextResource{TextResourceId = initialId++,TextIndex = "MustIndicateDayLes31",TextValue="Configuracion mensual: debe indicar un día del mes menor o igual que 31"},
                new TextResource{TextResourceId = initialId++,TextIndex = "MustIndicateDayOfMonth",TextValue="Configuracion mensual: debe indicar un día del mes"},
                new TextResource{TextResourceId = initialId++,TextIndex = "MustIndicateTypeEveryDay",TextValue="Configuracion mensual: debe indicar el tipo de cada día"},
                new TextResource{TextResourceId = initialId++,TextIndex = "MustIndicateTypeOfDayWeek",TextValue="Configuracion mensual: debe indicar el tipo de día de la semana"},
                new TextResource{TextResourceId = initialId++,TextIndex = "MustSelectDayWeek",TextValue="Configuración semanal: debe seleccionar un o mas días de la semana"},
                new TextResource{TextResourceId = initialId++,TextIndex = "NotCalculate",TextValue="No hay calculo"},
                new TextResource{TextResourceId = initialId++,TextIndex = "NotNextExecution",TextValue="No hay proxima ejecución"},
                new TextResource{TextResourceId = initialId++,TextIndex = "NumberMustGreaZero",TextValue="Número de repeticiones debe ser mayor que cero"},
                new TextResource{TextResourceId = initialId++,TextIndex = "OccursGreatZero",TextValue="'Ocurre cada' debe ser mayo o igual que cero"},
                new TextResource{TextResourceId = initialId++,TextIndex = "OnceAtValue",TextValue="'Ocurre en' debe tener un valor"},
                new TextResource{TextResourceId = initialId++,TextIndex = "StartDateInvalid",TextValue="Fecha de comienzo incorrecta"},
                new TextResource{TextResourceId = initialId++,TextIndex = "StartingAtNotHasValue",TextValue="'Comienza en' debe tener un valor"},
                new TextResource{TextResourceId = initialId++,TextIndex = "WrongConfiguration",TextValue="Configuración incorrecta"},
                new TextResource{TextResourceId = initialId++,TextIndex = "FormatterDailyFrecuency_TextEvery",TextValue="cada {0} {1} entre las {2} y las {3}"},
                new TextResource{TextResourceId = initialId++,TextIndex = "FormatterDailyFrecuency_TextOnce",TextValue="Ocurre una vez en {0}"},
                new TextResource{TextResourceId = initialId++,TextIndex = "FormatterMonthly_TextBase",TextValue="Ocurre {0} cada {1} meses"},
                new TextResource{TextResourceId = initialId++,TextIndex = "FormatterMonthly_TextTypeDay",TextValue="el {0}"},
                new TextResource{TextResourceId = initialId++,TextIndex = "FormatterMonthly_TextTypeEvery",TextValue="el {0} {1}"},
                new TextResource{TextResourceId = initialId++,TextIndex = "FormatterOnce_TextBase",TextValue="Ocurre una vez. El horario se utilizará el {0} a las {1} comenzando el {2}"},
                new TextResource{TextResourceId = initialId++,TextIndex = "FormatterRecurring_TextBase",TextValue="{0} empezando el {1}"},
                new TextResource{TextResourceId = initialId++,TextIndex = "FormatterRecurring_TextNoConfigWeekly",TextValue="Ocurre cada {0}{1}{2}. El horario se utilizará el {3} a las {4}"},
                new TextResource{TextResourceId = initialId++,TextIndex = "FormatterWeekly_TextAnd",TextValue=" y"},
                new TextResource{TextResourceId = initialId++,TextIndex = "FormatterWeekly_TextBase",TextValue="Ocurre cada {0} semanas en {1}"},
                new TextResource{TextResourceId = initialId++,TextIndex = "friday",TextValue="viernes"},
                new TextResource{TextResourceId = initialId++,TextIndex = "monday",TextValue="lunes"},
                new TextResource{TextResourceId = initialId++,TextIndex = "saturday",TextValue="sabado"},
                new TextResource{TextResourceId = initialId++,TextIndex = "sunday",TextValue="domingo"},
                new TextResource{TextResourceId = initialId++,TextIndex = "thursday",TextValue="jueves"},
                new TextResource{TextResourceId = initialId++,TextIndex = "tuesday",TextValue="martes"},
                new TextResource{TextResourceId = initialId++,TextIndex = "TypesEveryDayMonthly_Weekday",TextValue="entre semana"},
                new TextResource{TextResourceId = initialId++,TextIndex = "TypesEveryMonthly_First",TextValue="primer"},
                new TextResource{TextResourceId = initialId++,TextIndex = "TypesEveryMonthly_Fourth",TextValue="cuarto"},
                new TextResource{TextResourceId = initialId++,TextIndex = "TypesEveryMonthly_Last",TextValue="último"},
                new TextResource{TextResourceId = initialId++,TextIndex = "TypesEveryMonthly_Second",TextValue="segundo"},
                new TextResource{TextResourceId = initialId++,TextIndex = "TypesEveryMonthly_Third",TextValue="tercero"},
                new TextResource{TextResourceId = initialId++,TextIndex = "TypesEveryDayMonthly_Weekend",TextValue="fin de semana"},
                new TextResource{TextResourceId = initialId++,TextIndex = "TypesOccurs_Daily",TextValue="día"},
                new TextResource{TextResourceId = initialId++,TextIndex = "TypesOccurs_Monthly",TextValue="mes"},
                new TextResource{TextResourceId = initialId++,TextIndex = "TypesOccurs_Weekly",TextValue="semana"},
                new TextResource{TextResourceId = initialId++,TextIndex = "TypesUnitsDailyFrecuency_Hours",TextValue="horas"},
                new TextResource{TextResourceId = initialId++,TextIndex = "TypesUnitsDailyFrecuency_Minutes",TextValue="minutos"},
                new TextResource{TextResourceId = initialId++,TextIndex = "TypesUnitsDailyFrecuency_Seconds",TextValue="segundos"},
                new TextResource{TextResourceId = initialId++,TextIndex = "wednesday",TextValue="miercoles"},
                new TextResource{TextResourceId = initialId++,TextIndex = "EnumConversionError",TextValue="Error al convertir el valor {0} en {1}"},
                new TextResource{TextResourceId = initialId++,TextIndex = "NameConfigNotFound",TextValue="No se ha encontrado una configuración con el nombre{0}"},
                new TextResource{TextResourceId = initialId++,TextIndex = "NameRequired",TextValue="El nombre es obligatorio"},
                new TextResource{TextResourceId = initialId++,TextIndex = "NameDuplicate",TextValue="Ya existe otra configuración con el mismo nombre"}
            };
        }

        private static List<ResourceCulture> CreateCultures()
        {
            return new List<ResourceCulture>()
            {
                new ResourceCulture{  ResourceCultureId= 1, CultureName = "en-GB"},
                new ResourceCulture{ ResourceCultureId= 2, CultureName = "en-US"}
            };
        }

        private static List<TextResourceCulture> CreateEnglishTexts(int initialId, List<TextResource> textResources, ResourceCulture resourceCulture)
        {
            
            return new List<TextResourceCulture>()
            {
               ResourcesInitializer.CreateTextResourceCulture(initialId++,textResources,resourceCulture,"ConfigMustHasValue","Config must have a value"),
                ResourcesInitializer.CreateTextResourceCulture(initialId++,textResources,resourceCulture,"CurrentDateInvalid","The current date is invalid"),
                ResourcesInitializer.CreateTextResourceCulture(initialId++,textResources,resourceCulture,"DateTimeMustHasValue","Date Time must have a value"),
                ResourcesInitializer.CreateTextResourceCulture(initialId++,textResources,resourceCulture,"EndAtMinorStartingAt","'End at' must be great than 'Starting at'"),
                ResourcesInitializer.CreateTextResourceCulture(initialId++,textResources,resourceCulture,"EndAtNotHasValue","'End at' must have a value"),
                ResourcesInitializer.CreateTextResourceCulture(initialId++,textResources,resourceCulture,"EndDateAerlierCurrentDate","the end date cannot be earlier than the current date"),
                ResourcesInitializer.CreateTextResourceCulture(initialId++,textResources,resourceCulture,"EndDateGreatStartDate","End date must be great than start date"),
                ResourcesInitializer.CreateTextResourceCulture(initialId++,textResources,resourceCulture,"EveryMustGreatZero","Weekly configuration: 'Every' must be greater than zero"),
                ResourcesInitializer.CreateTextResourceCulture(initialId++,textResources,resourceCulture,"MonthBeBetween1And12","Montly configuration: month(s) must be between 1 and 12"),
                ResourcesInitializer.CreateTextResourceCulture(initialId++,textResources,resourceCulture,"MustIndicateDayGreatZero","Montly configuration: must indicate a day of the month great than zero"),
                ResourcesInitializer.CreateTextResourceCulture(initialId++,textResources,resourceCulture,"MustIndicateDayLes31","Montly configuration: must indicate a day of the month less or equal than 31"),
                ResourcesInitializer.CreateTextResourceCulture(initialId++,textResources,resourceCulture,"MustIndicateDayOfMonth","Montly configuration: must indicate a day of the month"),
                ResourcesInitializer.CreateTextResourceCulture(initialId++,textResources,resourceCulture,"MustIndicateTypeEveryDay","Montly configuration: must indicate the type every day"),
                ResourcesInitializer.CreateTextResourceCulture(initialId++,textResources,resourceCulture,"MustIndicateTypeOfDayWeek","Montly configuration: must indicate a type of day week"),
                ResourcesInitializer.CreateTextResourceCulture(initialId++,textResources,resourceCulture,"MustSelectDayWeek","Weekly configuration: must select one or more days of the week"),
                ResourcesInitializer.CreateTextResourceCulture(initialId++,textResources,resourceCulture,"NotCalculate","Scheluder not calculate"),
                ResourcesInitializer.CreateTextResourceCulture(initialId++,textResources,resourceCulture,"NotNextExecution","Not next execution time"),
                ResourcesInitializer.CreateTextResourceCulture(initialId++,textResources,resourceCulture,"NumberMustGreaZero","Number of occurs must be great than zero"),
                ResourcesInitializer.CreateTextResourceCulture(initialId++,textResources,resourceCulture,"OccursGreatZero","'Occurs every' must be greater or equal than zero"),
                ResourcesInitializer.CreateTextResourceCulture(initialId++,textResources,resourceCulture,"OnceAtValue","'Once at' must have a value"),
                ResourcesInitializer.CreateTextResourceCulture(initialId++,textResources,resourceCulture,"StartDateInvalid","Start Date is invalid"),
                ResourcesInitializer.CreateTextResourceCulture(initialId++,textResources,resourceCulture,"StartingAtNotHasValue","'Starting at' must have a value"),
                ResourcesInitializer.CreateTextResourceCulture(initialId++,textResources,resourceCulture,"WrongConfiguration","wrong configuration"),
                ResourcesInitializer.CreateTextResourceCulture(initialId++,textResources,resourceCulture,"FormatterDailyFrecuency_TextEvery","ever {0} {1} between {2} and {3}"),
                ResourcesInitializer.CreateTextResourceCulture(initialId++,textResources,resourceCulture,"FormatterDailyFrecuency_TextOnce","occurs once at {0}"),
                ResourcesInitializer.CreateTextResourceCulture(initialId++,textResources,resourceCulture,"FormatterMonthly_TextBase","Occurs {0} of very {1} months"),
                ResourcesInitializer.CreateTextResourceCulture(initialId++,textResources,resourceCulture,"FormatterMonthly_TextTypeDay","the {0}"),
                ResourcesInitializer.CreateTextResourceCulture(initialId++,textResources,resourceCulture,"FormatterMonthly_TextTypeEvery","the {0} {1}"),
                ResourcesInitializer.CreateTextResourceCulture(initialId++,textResources,resourceCulture,"FormatterOnce_TextBase","Occurs once. Schedule will be used on {0} at {1} starting on {2}"),
                ResourcesInitializer.CreateTextResourceCulture(initialId++,textResources,resourceCulture,"FormatterRecurring_TextBase","{0} starting on {1}"),
                ResourcesInitializer.CreateTextResourceCulture(initialId++,textResources,resourceCulture,"FormatterRecurring_TextNoConfigWeekly","Occurs every {0}{1}{2}. Schedule will be used on {3} at {4}"),
                ResourcesInitializer.CreateTextResourceCulture(initialId++,textResources,resourceCulture,"FormatterWeekly_TextAnd"," and "),
                ResourcesInitializer.CreateTextResourceCulture(initialId++,textResources,resourceCulture,"FormatterWeekly_TextBase","Occurs every {0} weeks on {1}"),
                ResourcesInitializer.CreateTextResourceCulture(initialId++,textResources,resourceCulture,"friday","friday"),
                ResourcesInitializer.CreateTextResourceCulture(initialId++,textResources,resourceCulture,"monday","monday"),
                ResourcesInitializer.CreateTextResourceCulture(initialId++,textResources,resourceCulture,"saturday","saturday"),
                ResourcesInitializer.CreateTextResourceCulture(initialId++,textResources,resourceCulture,"sunday","sunday"),
                ResourcesInitializer.CreateTextResourceCulture(initialId++,textResources,resourceCulture,"thursday","thursday"),
                ResourcesInitializer.CreateTextResourceCulture(initialId++,textResources,resourceCulture,"tuesday","tuesday"),
                ResourcesInitializer.CreateTextResourceCulture(initialId++,textResources,resourceCulture,"TypesEveryDayMonthly_Weekday","weekday"),
                ResourcesInitializer.CreateTextResourceCulture(initialId++,textResources,resourceCulture,"TypesEveryMonthly_First","first"),
                ResourcesInitializer.CreateTextResourceCulture(initialId++,textResources,resourceCulture,"TypesEveryMonthly_Fourth","fourth"),
                ResourcesInitializer.CreateTextResourceCulture(initialId++,textResources,resourceCulture,"TypesEveryMonthly_Last","last"),
                ResourcesInitializer.CreateTextResourceCulture(initialId++,textResources,resourceCulture,"TypesEveryMonthly_Second","second"),
                ResourcesInitializer.CreateTextResourceCulture(initialId++,textResources,resourceCulture,"TypesEveryMonthly_Third","third"),
                ResourcesInitializer.CreateTextResourceCulture(initialId++,textResources,resourceCulture,"TypesEveryDayMonthly_Weekend","weekend"),
                ResourcesInitializer.CreateTextResourceCulture(initialId++,textResources,resourceCulture,"TypesOccurs_Daily","daily"),
                ResourcesInitializer.CreateTextResourceCulture(initialId++,textResources,resourceCulture,"TypesOccurs_Monthly","monthly"),
                ResourcesInitializer.CreateTextResourceCulture(initialId++,textResources,resourceCulture,"TypesOccurs_Weekly","weekly"),
                ResourcesInitializer.CreateTextResourceCulture(initialId++,textResources,resourceCulture,"TypesUnitsDailyFrecuency_Hours","hours"),
                ResourcesInitializer.CreateTextResourceCulture(initialId++,textResources,resourceCulture,"TypesUnitsDailyFrecuency_Minutes","minutes"),
                ResourcesInitializer.CreateTextResourceCulture(initialId++,textResources,resourceCulture,"TypesUnitsDailyFrecuency_Seconds","seconds"),
                ResourcesInitializer.CreateTextResourceCulture(initialId++,textResources,resourceCulture,"wednesday","wednesday"),
                ResourcesInitializer.CreateTextResourceCulture(initialId++,textResources,resourceCulture,"EnumConversionError","Error to convert {0} at {1}"),
                ResourcesInitializer.CreateTextResourceCulture(initialId++,textResources,resourceCulture,"NameConfigNotFound","Config with name '{0}' not found"),
                ResourcesInitializer.CreateTextResourceCulture(initialId++, textResources, resourceCulture,"NameRequired","The name is required"),
                ResourcesInitializer.CreateTextResourceCulture(initialId++,textResources,resourceCulture,"NameDuplicate","There is already configuration with the same name")
            };
        }

        private static TextResourceCulture CreateTextResourceCulture(int initialId, List<TextResource> textResources, ResourceCulture resorceCulture, string indexValue, string value)
        {
            return new TextResourceCulture
            {
                TextResourceCultureId = initialId,
                TextResourceId = ResourcesInitializer.GetTextResource(textResources, indexValue).TextResourceId,
                ResourceCultureId = resorceCulture.ResourceCultureId,
                TextValue = value
            };
        }
        private static TextResource GetTextResource(List<TextResource> textResources, string indexValue)
        {
            return textResources.FirstOrDefault(T => T.TextIndex == indexValue); 
        }



    }


}
