using Formacion.Formatters;
using Formacion.TextsTranslations;
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
                    throw new ApplicationException(Translator.GetText(TextsIndex.NotCalculate));
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
