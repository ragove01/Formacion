using Formacion.Views;
using System;
using System.Globalization;
using System.Threading;

namespace Formacion.Formatters
{
    public class FormatterBase
    {
        protected readonly SchedulerConfig Config;
        public FormatterBase(SchedulerConfig theConfig)
        {
            this.Config = theConfig;
            SetCulture(theConfig?.Culture);
        }

        public virtual string Formatter(DateTime nextExecution)
        {
            throw new NotImplementedException();
        }

        public static void SetCulture(CultureInfo Culture)
        {
            if(Culture == null)
            {
                return;
            }
            if (CultureInfo.CurrentCulture != Culture)
            {
                Thread.CurrentThread.CurrentCulture = Culture;
                Thread.CurrentThread.CurrentUICulture = Culture;
            }
        }
    }
}
