using Formacion.Enums;
using Formacion.TextsTranslations;
using Formacion.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Formacion.Formatters
{
    public class FormatterMonthly : FormatterBase
    {
        

        public FormatterMonthly(SchedulerConfig config) : base(config)
        {

        }

        public override string Formatter(DateTime nextExecution)
        {
            if (!this.HasConfig()) { return string.Empty; }
            if (!this.HasConfigMonthly()) { return string.Empty; }
            return string.Format(
                Translator.GetText(TextsIndex.FormatterMonthly_TextBase), this.FormatterType(), this.Config.Monthly.EveryNumberMonths);
        }

        private bool HasConfig()
        {
            return this.Config != null;
        }

        private bool HasConfigMonthly()
        {
            if (this.Config.Monthly == null)
            {
                return false;
            }
            return this.Config.Monthly.TypesDayEvery.HasValue || 
                this.Config.Monthly.TypesEvery.HasValue ||
                (this.Config.Monthly.Type == Enums.TypesMontlyFrecuency.Day && this.Config.Monthly.DayMonth.HasValue);
        }

        private string FormatterType()
        {
            if(this.Config.Monthly.Type == Enums.TypesMontlyFrecuency.Day)
            {
                return this.FormatterTypeDay(); 
            }
            return this.FormatterTypeEvery(); 
        }

        private string FormatterTypeDay()
        {
            return string.Format(Translator.GetText(TextsIndex.FormatterMonthly_TextTypeDay), this.Config.Monthly.DayMonth);
            
        }
        private string FormatterTypeEvery()
        {
            return string.Format(Translator.GetText(TextsIndex.FormatterMonthly_TextTypeEvery),
                this.GetStringEnum(this.Config.Monthly.TypesEvery.Value),
                this.GetStringEnumTypesDayEvery(this.Config.Monthly.TypesDayEvery.Value));
        }

        private string GetStringEnumTypesDayEvery(TypesEveryDayMonthly value)
        {
           if(value == TypesEveryDayMonthly.Weekday ||
                value == TypesEveryDayMonthly.Weekend)
            {
                return this.GetStringEnum(value);
            }
            return Translator.GetText(value.ToString().ToLower());
        }
       
    }
}
