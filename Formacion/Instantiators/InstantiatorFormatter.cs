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
            
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }
          
            if (config.Type == TypesSchedule.Once)
            {
                return GetFormaterOnce(config);
            }

            return GetFormaterRecurring(config);

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
