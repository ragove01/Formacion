using Formacion.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Formacion.Formatters
{
    public class FormatterMonthly : FormatterBase
    {
        private static string[] dayOfWeekNames = { "monday", "tuesday", "wednesday", "thursday", "friday", "saturday", "sunday" };

        public FormatterMonthly(SchedulerConfig TheConfig) : base(TheConfig)
        {

        }

        public override string Formatter(DateTime nextExecution)
        {
            if (!this.HasConfig()) { return string.Empty; }
            if (!this.HasConfigMonthly()) { return string.Empty; }
            return $"Occurs {this.FormatterType()} of very {this.Config.Monthly.EveryNumberMonths} months";

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
            return $"the {this.Config.Monthly.DayMonth}";
        }
        private string FormatterTypeEvery()
        {
            return $"the {this.Config.Monthly.TypesEvery.ToString().ToLower()} {this.Config.Monthly.TypesDayEvery.ToString().ToLower()}";
        }
       
    }
}
