using Formacion.Enums;
using Formacion.Views;
using System;

namespace Formacion.Formatters
{
    public class FormatterDailyFrequency : FormatterBase
    {


        public FormatterDailyFrequency(SchedulerConfig TheConfig):base(TheConfig)
        {
        }
        public override string Formatter(DateTime nextExecution)
        {
            if(this.Config.DailyFrecuenci == null)
            {
                return string.Empty;
            }
            if(this.Config.DailyFrecuenci.Frecuenci == TypesOccursDailyFrecuency.Once)
            {
                return this.FormatterOnce(); 
            }
            return this.FormatterEvery();
        }

        private string FormatterOnce()
        {
            return $" occurs once at {this.Config.DailyFrecuenci.OnceTime.Value.ToString("hh\\:mm")}";
        }
        private string FormatterEvery()
        {
            return $" ever {this.Config.DailyFrecuenci.NumberOccurs} {this.Config.DailyFrecuenci.TypeUnit.ToString().ToLower()}" +
                $" between {this.Config.DailyFrecuenci.StartTime.Value.ToString("hh\\:mm")} and" +
                $" {this.Config.DailyFrecuenci.EndTime.Value.ToString("hh\\:mm")}";
        }
    }
}
