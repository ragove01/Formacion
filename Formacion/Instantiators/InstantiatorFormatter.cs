using Formacion.Enums;
using Formacion.Formatters;
using Formacion.Views;
using System;

namespace Formacion.Instantiators
{
    public class InstantiatorFormatter
    {
        
        public static FormatterBase GetFormatter(SchedulerConfig config)
        {
            if(config.Type == TypesSchedule.Once)
            {
                return GetFormaterOnce(config);
            }
            if(config.Type == TypesSchedule.Recurring)
            {
                return GetFormaterRecurring(config);
            }
            throw new ApplicationException("Formatter not implemented");
        }
        private static FormatterBase GetFormaterOnce(SchedulerConfig config)
        {
            
                return new FormatterOnce(config);
           
        }
        private static FormatterBase GetFormaterRecurring(SchedulerConfig config)
        {

            return new FormatterRecurring(config);

        }
    }
}
