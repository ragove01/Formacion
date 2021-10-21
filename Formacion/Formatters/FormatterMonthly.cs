using Formacion.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Formacion.Formatters
{
    public class FormatterMonthly : FormatterBase
    {
        

        public FormatterMonthly(SchedulerConfig TheConfig) : base(TheConfig)
        {

        }

        public override string Formatter(DateTime nextExecution)
        {
            if (!this.HasConfig()) { return string.Empty; }
            if (!this.HasConfigMonthly()) { return string.Empty; }
            return string.Format(
                Texts.FormatterMonthly_TextBase, this.FormatterType(), this.Config.Monthly.EveryNumberMonths);
                //$"Occurs {this.FormatterType()} of very {this.Config.Monthly.EveryNumberMonths} months";

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
            return string.Format(Texts.FormatterMonthly_TextTypeDay, this.Config.Monthly.DayMonth);
                //$"the {this.Config.Monthly.DayMonth}";
        }
        private string FormatterTypeEvery()
        {
            return string.Format(Texts.FormatterMonthly_TextTypeEvery,
                this.Config.Monthly.TypesEvery.ToString().ToLower(),
                this.Config.Monthly.TypesDayEvery.ToString().ToLower());
                //$"the {this.Config.Monthly.TypesEvery.ToString().ToLower()} {this.Config.Monthly.TypesDayEvery.ToString().ToLower()}";
        }
       
    }
}
