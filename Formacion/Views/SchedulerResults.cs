using Formacion.Formatters;
using Formacion.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Formacion.Views
{
    public class SchedulerResults
    {
        private readonly FormatterBase formatter;
        public SchedulerResults(FormatterBase TheFormatter)
        {
            this.formatter = TheFormatter;
        }

       
      
        public string NextExecutionTimeString
        {
            get
            {
                if (this.NextExecution.HasValue == false)
                {
                    throw new ApplicationException("Scheluder not calculate");
                }
                return this.formatter.Formatter(this.NextExecution.Value);
            }
        }
        public DateTime? NextExecution
        {
            get;set;
        }
    }
}
