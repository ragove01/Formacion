using System;
using System.Globalization;
using System.Threading;
using Formacion.Instantiators;
using Formacion.Views;

namespace Formacion
{
    public class ScheluderGenerator
    {
       
        public SchedulerResults Calculate(DateTime currentDate, SchedulerConfig scheduleConfig)
        {
            SetCulture(scheduleConfig?.Culture);
            return new SchedulerResults(Instantiators.InstantiatorFormatter.GetFormatter(scheduleConfig))
            {
                NextExecution = InstantiatorCalculator.GetCalculator(scheduleConfig.Type).
                    Calculate(currentDate, scheduleConfig)
            };
        }

        public static void SetCulture(CultureInfo Culture)
        {
            
            if (Culture != null && CultureInfo.CurrentCulture != Culture)
            {
                Thread.CurrentThread.CurrentCulture = Culture;
                Thread.CurrentThread.CurrentUICulture = Culture;
            }
        }


    }
}
