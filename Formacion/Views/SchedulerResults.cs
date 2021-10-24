using Formacion.Formatters;
using System;

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
                    throw new ApplicationException(Validators.Texts.NotCalculate);
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
