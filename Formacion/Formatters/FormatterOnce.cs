using Formacion.TextsTranslations;
using Formacion.Views;
using System;

namespace Formacion.Formatters
{
    public class FormatterOnce: FormatterBase
    {

        public FormatterOnce(SchedulerConfig config):base(config)
        {

        }

        public override string Formatter(DateTime nextExecution)
        {
            return string.Format(Translator.GetText(TextsIndex.FormatterOnce_TextBase),
                nextExecution.ToString("d"), nextExecution.ToString("HH:mm"), this.Config.StartDate.ToString("d"));
           
        }
    }
}
