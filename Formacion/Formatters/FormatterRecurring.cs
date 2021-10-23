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
            return string.Format(Texts.FormatterRecurring_TextBase,
                this.FormatterReccurringPrivate(nextExecutionTime),
                this.Config.StartDate.ToString("d"));
        }

        private string FormatterReccurringPrivate(DateTime nextExecutionTime)
        {
            if(this.Config.Weekly != null)
            {
                return this.FormatterConfigWeekly(nextExecutionTime);
            }
            if(this.Config.Monthly != null)
            {
                return this.FormatterConfigMonthly(nextExecutionTime);
            }
            return this.FormatterNoConfigWeekly(nextExecutionTime);
        }



        private string FormatterNoConfigWeekly(DateTime nextExecutionTime)
        {
            return string.Format(Texts.FormatterRecurring_TextNoConfigWeekly,
                (this.Config.NumberOccurs > 1 ? this.Config.NumberOccurs.ToString() + " " : string.Empty),
                this.GetStringEnum(this.Config.Occurs),
                this.FormatterConfigDailyFrecuency(nextExecutionTime,true),
                nextExecutionTime.ToString("d"), nextExecutionTime.ToString("HH:mm"));
 
        }

        private string FormatterConfigWeekly(DateTime nextExecutionTime)
        {
            return $"{new FormatterWeekly(this.Config).Formatter(nextExecutionTime)}{this.FormatterConfigDailyFrecuency(nextExecutionTime,false)}.";
        }

        private string FormatterConfigMonthly(DateTime nextExecutionTime)
        {
            return $"{new FormatterMonthly(this.Config).Formatter(nextExecutionTime)}{this.FormatterConfigDailyFrecuency(nextExecutionTime,false)}.";
        }
        private string FormatterConfigDailyFrecuency(DateTime nextExecutionTime, bool startWithWhiteSpace)
        {
            string valueReturn = new FormatterDailyFrequency(this.Config).Formatter(nextExecutionTime);
            if(string.IsNullOrEmpty(valueReturn))
            {
                return string.Empty;
            }
            return $" {valueReturn}";  
        }
       
    }
}
