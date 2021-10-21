using Formacion.Views;
using System;

namespace Formacion.Formatters
{
    public class FormatterOnce: FormatterBase
    {

        public FormatterOnce(SchedulerConfig TheConfig):base(TheConfig)
        {

        }

        public override string Formatter(DateTime nextExecution)
        {
            return string.Format(Texts.FormatterOnce_TextBase,
                nextExecution.ToString("d"), nextExecution.ToString("HH:mm"), this.Config.StartDate.ToString("d"));
            //return $"Occurs once. Schedule will be used on {nextExecution.ToString("d")} at {nextExecution.ToString("HH:mm")}" +
            //    $" starting on {nextExecution.ToString("HH:mm")}";
        }
    }
}
