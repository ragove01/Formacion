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

        private static TimeSpan GetTimeConfigDaily(ConfigDailyFrecuency DailyFrecuenci)
        {
            if (DailyFrecuenci == null)
            {
                return new TimeSpan(0);
            }
            if (DailyFrecuenci.Frecuenci == TypesOccursDailyFrecuency.Every &&
                DailyFrecuenci.EndTime.HasValue)
            {
                return DailyFrecuenci.EndTime.Value;
            }
            if (DailyFrecuenci.Frecuenci == TypesOccursDailyFrecuency.Once &&
                DailyFrecuenci.OnceTime.HasValue)
            {
                return DailyFrecuenci.OnceTime.Value;
            }
            return new TimeSpan(0);
        }
        
    }
}
