using Formacion.Views;
using System;

namespace Formacion.Formatters
{
    public class FormatterBase
    {
        protected readonly SchedulerConfig Config;
        public FormatterBase(SchedulerConfig theConfig)
        {
            this.Config = theConfig;
        }

        public virtual string Formatter(DateTime nextExecution)
        {
            throw new NotImplementedException();
        }
    }
}
