using Formacion.Enums;
using Formacion.TextsTranslations;
using Formacion.Views;
using System;

namespace Formacion.Formatters
{
    public class FormatterDailyFrequency : FormatterBase
    {


        public FormatterDailyFrequency(SchedulerConfig config):base(config)
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
            return string.Format(Translator.GetText(TextsIndex.FormatterDailyFrecuency_TextOnce),
                this.Config.DailyFrecuenci.OnceTime.Value.ToString("hh\\:mm"));
               
        }
        private string FormatterEvery()
        {
            return string.Format(Translator.GetText(TextsIndex.FormatterDailyFrecuency_TextEvery),
                this.Config.DailyFrecuenci.NumberOccurs,
                this.GetStringEnum(this.Config.DailyFrecuenci.TypeUnit),
                this.Config.DailyFrecuenci.StartTime.Value.ToString("hh\\:mm"),
                this.Config.DailyFrecuenci.EndTime.Value.ToString("hh\\:mm"));
 
        }

    }
}
