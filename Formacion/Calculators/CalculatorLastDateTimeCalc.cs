using Formacion.Configs;
using Formacion.Enums;
using Formacion.Extensions;
using Formacion.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Formacion.Calculators
{
    public class CalculatorLastDateTimeCalc
    {
        public static DateTime GetLastDateTime(SchedulerConfig config)
        {
            if(config.EndDate.HasValue == false)
            {
                return DateTime.MaxValue;
            }
            return config.EndDate.Value.SetTimeToDate(CalculatorLastDateTimeCalc.GetTimeConfigDaily(config.DailyFrecuenci));
        }

        private static TimeSpan GetTimeConfigDaily(ConfigDailyFrecuency configDailyFrecuenci)
        {
            if (configDailyFrecuenci == null)
            {
                return new TimeSpan(0);
            }
            if (configDailyFrecuenci.Frecuenci == TypesOccursDailyFrecuency.Every &&
                configDailyFrecuenci.EndTime.HasValue)
            {
                return configDailyFrecuenci.EndTime.Value;
            }
            if (configDailyFrecuenci.Frecuenci == TypesOccursDailyFrecuency.Once &&
                configDailyFrecuenci.OnceTime.HasValue)
            {
                return configDailyFrecuenci.OnceTime.Value;
            }
            return new TimeSpan(0);
        }
        
    }
}
