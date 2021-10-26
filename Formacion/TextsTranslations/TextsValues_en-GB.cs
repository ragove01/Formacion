using System;
using System.Collections.Generic;
using System.Text;

namespace Formacion.TextsTranslations
{
    internal class TextsValues_en_GB:TextValuesBase 
    {
        protected override Dictionary<TextsIndex, string> LoadTexts()
        {
            return new Dictionary<TextsIndex, string>() {
            {TextsIndex.ConfigMustHasValue,"Config must have a value"},
            {TextsIndex.CurrentDateInvalid,"The current date is invalid"},
            {TextsIndex.DateTimeMustHasValue,"Date Time must have a value"},
            {TextsIndex.EndAtMinorStartingAt,"'End at' must be great than 'Starting at'"},
            {TextsIndex.EndAtNotHasValue,"'End at' must have a value"},
            {TextsIndex.EndDateAerlierCurrentDate,"the end date cannot be earlier than the current date"},
            {TextsIndex.EndDateGreatStartDate,"End date must be great than start date"},
            {TextsIndex.EveryMustGreatZero,"Weekly configuration: 'Every' must be greater than zero"},
            {TextsIndex.MonthBeBetween1And12,"Montly configuration: month(s) must be between 1 and 12"},
            {TextsIndex.MustIndicateDayGreatZero,"Montly configuration: must indicate a day of the month great than zero"},
            {TextsIndex.MustIndicateDayLes31,"Montly configuration: must indicate a day of the month less or equal than 31"},
            {TextsIndex.MustIndicateDayOfMonth,"Montly configuration: must indicate a day of the month"},
            {TextsIndex.MustIndicateTypeEveryDay,"Montly configuration: must indicate the type every day"},
            {TextsIndex.MustIndicateTypeOfDayWeek,"Montly configuration: must indicate a type of day week"},
            {TextsIndex.MustSelectDayWeek,"Weekly configuration: must select one or more days of the week"},
            {TextsIndex.NotCalculate,"Scheluder not calculate"},
            {TextsIndex.NotNextExecution,"Not next execution time"},
            {TextsIndex.NumberMustGreaZero,"Number of occurs must be great than zero"},
            {TextsIndex.OccursGreatZero,"'Occurs every' must be greater or equal than zero"},
            {TextsIndex.OnceAtValue,"'Once at' must have a value"},
            {TextsIndex.StartDateInvalid,"Start Date is invalid"},
            {TextsIndex.StartingAtNotHasValue,"'Starting at' must have a value"},
            {TextsIndex.WrongConfiguration,"wrong configuration"},
            {TextsIndex.FormatterDailyFrecuency_TextEvery,"ever {0} {1} between {2} and {3}"},
            {TextsIndex.FormatterDailyFrecuency_TextOnce,"occurs once at {0}"},
            {TextsIndex.FormatterMonthly_TextBase,"Occurs {0} of very {1} months"},
            {TextsIndex.FormatterMonthly_TextTypeDay,"the {0}"},
            {TextsIndex.FormatterMonthly_TextTypeEvery,"the {0} {1}"},
            {TextsIndex.FormatterOnce_TextBase,"Occurs once. Schedule will be used on {0} at {1} starting on {2}"},
            {TextsIndex.FormatterRecurring_TextBase,"{0} starting on {1}"},
            {TextsIndex.FormatterRecurring_TextNoConfigWeekly,"Occurs every {0}{1}{2}. Schedule will be used on {3} at {4}"},
            {TextsIndex.FormatterWeekly_TextAnd,"and"},
            {TextsIndex.FormatterWeekly_TextBase,"Occurs every {0} weeks on {1}"},
            {TextsIndex.friday,"friday"},
            {TextsIndex.monday,"monday"},
            {TextsIndex.saturday,"saturday"},
            {TextsIndex.sunday,"sunday"},
            {TextsIndex.thursday,"thursday"},
            {TextsIndex.tuesday,"tuesday"},
            {TextsIndex.TypesEveryDayMonthly_Weekday,"weekday"},
            {TextsIndex.TypesEveryMonthly_First,"first"},
            {TextsIndex.TypesEveryMonthly_Fourth,"fourth"},
            {TextsIndex.TypesEveryMonthly_Last,"last"},
            {TextsIndex.TypesEveryMonthly_Second,"second"},
            {TextsIndex.TypesEveryMonthly_Third,"third"},
            {TextsIndex.TypesEveyDayMonthly_WeekEndDay,"weekendday"},
            {TextsIndex.TypesOccurs_Daily,"daily"},
            {TextsIndex.TypesOccurs_Monthly,"monthly"},
            {TextsIndex.TypesOccurs_Weekly,"weekly"},
            {TextsIndex.TypesUnitsDailyFrecuency_Hours,"hours"},
            {TextsIndex.TypesUnitsDailyFrecuency_Minutes,"minutes"},
            {TextsIndex.TypesUnitsDailyFrecuency_Seconds,"seconds"},
            {TextsIndex.wednesday,"wednesday"}};
        }
    }
}
