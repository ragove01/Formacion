using System;
using System.Globalization;
using System.Threading;
using Formacion.Instantiators;
using Formacion.Views;

namespace Formacion
{
    public class SchedulerGenerator
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

        public static void SetCulture(CultureInfo culture)
        {

            if (culture != null && CultureInfo.CurrentCulture != culture)
            {
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
            }
        }
    }
}
