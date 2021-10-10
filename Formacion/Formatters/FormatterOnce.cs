using Formacion.Interfaces;
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
            return $"Occurs once. Schedule will be used on {nextExecution.ToString("dd/MM/yyyy")} at {nextExecution.ToString("HH:mm")}" +
                $" starting on {this.Config.StartDate.ToString("dd/MM/yyyy")}";
        }
    }
}
