using Formacion.Views;
using System;

namespace Formacion.Formatters
{
    public class FormatterRecurring:FormatterBase 
    {
     
        
        public FormatterRecurring(SchedulerConfig TheConfig):base(TheConfig)
        {
 
        }


        public override string Formatter(DateTime nextExecutionTime)
        {
            return $"{this.FormatterReccurringPrivate(nextExecutionTime)}" +
                $" starting on {this.Config.StartDate.ToString("dd/MM/yyyy")}";
        }

        private string FormatterReccurringPrivate(DateTime nextExecutionTime)
        {
            return this.Config.Weekly == null ? this.FormatterNoConfigWeekly(nextExecutionTime) :
                    this.FormatterConfigWeekly(nextExecutionTime);
        }



        private string FormatterNoConfigWeekly(DateTime nextExecutionTime)
        {
            return $"Occurs every {(this.Config.NumberOccurs>1?" " + this.Config.NumberOccurs.ToString():string.Empty)}{this.Config.Occurs.ToString().ToLower()}{this.FormatterConfigDailyFrecuency(nextExecutionTime)}. " +
                $"Schedule will be used on {nextExecutionTime.ToString("dd/MM/yyyy")} at {nextExecutionTime.ToString("HH:mm")}";
        }

        private string FormatterConfigWeekly(DateTime nextExecutionTime)
        {
            return $"{new FormatterWeekly(this.Config).Formatter(nextExecutionTime)}{this.FormatterConfigDailyFrecuency(nextExecutionTime)}.";
        }
        private string FormatterConfigDailyFrecuency(DateTime nextExecutionTime)
        {
            return new FormatterDailyFrequency(this.Config).Formatter(nextExecutionTime);  
        }

    }
}
