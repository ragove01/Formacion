using Formacion.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Formacion.Views
{
    public class SchedulerResults
    {
        private readonly IResultFormatter formatter;
        public SchedulerResults(IResultFormatter TheFormatter)
        {
            this.formatter = TheFormatter;
        }
        public IResult[] Results { get; set; }
        public string NextExecutionTimeString
        {
            get
            {
                if (this.Results == null || this.Results.Length == 0)
                {
                    throw new ApplicationException("Scheluder not calculate");
                }
                return this.formatter.Formatter(this.Results.FirstOrDefault());
            }
        }
        public DateTime? NextExecution
        {
            get
            {
                if (this.Results == null)
                {
                    throw new ApplicationException("Scheluder not calculate");
                }
                return this.Results.FirstOrDefault()?.NextExecution;
            }
        }
    }
}
