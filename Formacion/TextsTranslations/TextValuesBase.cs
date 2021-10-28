using System.Collections.Generic;
using System.Globalization;

namespace Formacion.TextsTranslations
{
    internal class TextValuesBase
    {
        protected Dictionary<TextsIndex, string> textsValues;
        internal readonly CultureInfo Culture;

        internal TextValuesBase():this(CultureInfo.GetCultureInfo("es-ES"))
        {
            
            
        }
        protected TextValuesBase(CultureInfo culture)
        {
            this.Culture = culture;
            this.textsValues = this.LoadTexts();
        }
        internal string GetText(TextsIndex text)
        {
            return this.textsValues[text];
        }

        protected virtual Dictionary<TextsIndex, string> LoadTexts()
        {
            return new Dictionary<TextsIndex, string>()
            {{TextsIndex.ConfigMustHasValue,"Configuración debe tener un valor"},
            {TextsIndex.CurrentDateInvalid,"La fecha actual es incorrecta"},
            {TextsIndex.DateTimeMustHasValue,"La fecha debe tener un valor"},
            {TextsIndex.EndAtMinorStartingAt,"'Finaliza en' debe ser mas grande que 'Comienza en'"},
            {TextsIndex.EndAtNotHasValue,"'Finaliza en' debe tener un valor"},
            {TextsIndex.EndDateAerlierCurrentDate,"La fecha final no puede ser menor que la fecha actual"},
            {TextsIndex.EndDateGreatStartDate,"Fecha de finalización debe ser mayor que la fecha de comienzo"},
            {TextsIndex.EveryMustGreatZero,"Configuración semanal: 'Cada' debe ser mayor que zero"},
            {TextsIndex.MonthBeBetween1And12,"Configuracion mensual: mes(s) debe estar entre 1 y 12"},
            {TextsIndex.MustIndicateDayGreatZero,"Configuracion mensual: debe indicar un día del mes mayor que cero"},
            {TextsIndex.MustIndicateDayLes31,"Configuracion mensual: debe indicar un día del mes menor o igual que 31"},
            {TextsIndex.MustIndicateDayOfMonth,"Configuracion mensual: debe indicar un día del mes"},
            {TextsIndex.MustIndicateTypeEveryDay,"Configuracion mensual: debe indicar el tipo de cada día"},
            {TextsIndex.MustIndicateTypeOfDayWeek,"Configuracion mensual: debe indicar el tipo de día de la semana"},
            {TextsIndex.MustSelectDayWeek,"Configuración semanal: debe seleccionar un o mas días de la semana"},
            {TextsIndex.NotCalculate,"No hay calculo"},
            {TextsIndex.NotNextExecution,"No hay proxima ejecución"},
            {TextsIndex.NumberMustGreaZero,"Número de repeticiones debe ser mayor que cero"},
            {TextsIndex.OccursGreatZero,"'Ocurre cada' debe ser mayo o igual que cero"},
            {TextsIndex.OnceAtValue,"'Ocuerre en' debe tener un valor"},
            {TextsIndex.StartDateInvalid,"Fecha de comienzo incorrecta"},
            {TextsIndex.StartingAtNotHasValue,"'Comienza en' debe tener un valor"},
            {TextsIndex.WrongConfiguration,"Configuración incorrecta"},
            {TextsIndex.FormatterDailyFrecuency_TextEvery,"cada {0} {1} entre las {2} y las {3}"},
            {TextsIndex.FormatterDailyFrecuency_TextOnce,"Ocurre una vez en {0}"},
            {TextsIndex.FormatterMonthly_TextBase,"Ocurre {0} cada {1} meses"},
            {TextsIndex.FormatterMonthly_TextTypeDay,"el {0}"},
            {TextsIndex.FormatterMonthly_TextTypeEvery,"el {0} {1}"},
            {TextsIndex.FormatterOnce_TextBase,"Ocurre una vez. El horario se utilizará el {0} a las {1} comenzando el {2}"},
            {TextsIndex.FormatterRecurring_TextBase,"{0} empezando el {1}"},
            {TextsIndex.FormatterRecurring_TextNoConfigWeekly,"Ocurre cada {0}{1}{2}. El horario se utilizará el {3} a las {4}"},
            {TextsIndex.FormatterWeekly_TextAnd," y"},
            {TextsIndex.FormatterWeekly_TextBase,"Ocurre cada {0} semanas en {1}"},
            {TextsIndex.friday,"viernes"},
            {TextsIndex.monday,"lunes"},
            {TextsIndex.saturday,"sabado"},
            {TextsIndex.sunday,"domingo"},
            {TextsIndex.thursday,"jueves"},
            {TextsIndex.tuesday,"martes"},
            {TextsIndex.TypesEveryDayMonthly_Weekday,"entre semana"},
            {TextsIndex.TypesEveryMonthly_First,"primer"},
            {TextsIndex.TypesEveryMonthly_Fourth,"cuarto"},
            {TextsIndex.TypesEveryMonthly_Last,"último"},
            {TextsIndex.TypesEveryMonthly_Second,"segundo"},
            {TextsIndex.TypesEveryMonthly_Third,"tercero"},
            {TextsIndex.TypesEveryDayMonthly_Weekend,"fin de semana"},
            {TextsIndex.TypesOccurs_Daily,"día"},
            {TextsIndex.TypesOccurs_Monthly,"mes"},
            {TextsIndex.TypesOccurs_Weekly,"semana"},
            {TextsIndex.TypesUnitsDailyFrecuency_Hours,"horas"},
            {TextsIndex.TypesUnitsDailyFrecuency_Minutes,"minutos"},
            {TextsIndex.TypesUnitsDailyFrecuency_Seconds,"segundos"},
            {TextsIndex.wednesday,"miercoles"},
            {TextsIndex.EnumConversionError,"Error al convertir el valor {0} en {1}"}};

        }
    
    }
}
