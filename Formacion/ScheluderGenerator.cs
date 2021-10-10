using System;
using Formacion.Instantiators;
using Formacion.Views;

namespace Formacion
{
    public class ScheluderGenerator
    {
       
        public SchedulerResults Calculate(DateTime currentDate, SchedulerConfig scheduleConfig)
        {
            return new SchedulerResults(Instantiators.InstantiatorFormatter.GetFormatter(scheduleConfig))
            {
                NextExecution = InstantiatorCalculator.GetCalculator(scheduleConfig.Type).
                    Calculate(currentDate, scheduleConfig)
            };
        }

        

     
    }
}
