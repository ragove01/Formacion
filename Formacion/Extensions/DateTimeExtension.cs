using Formacion.Enums;
using System;

namespace Formacion.Extensions
{
    public static class DateTimeExtension
    {
        public static DateTime SetTimeToDate(this DateTime dayDate, TimeSpan time)
        {
            return new DateTime(dayDate.Year, dayDate.Month, dayDate.Day, time.Hours, time.Minutes, time.Seconds);
        }

        public static DateTime AddInteval(this DateTime dateTime, TypesUnitsDailyFrecuency type, int units)
        {
            return dateTime.Add(DateTimeExtension.GetInterval(type, units));
        }

        private static TimeSpan GetInterval(TypesUnitsDailyFrecuency type, int units)
        {
            switch (type)
            {
                case TypesUnitsDailyFrecuency.Hours:
                    return new TimeSpan(units, 0, 0);
                case TypesUnitsDailyFrecuency.Minutes:
                    return new TimeSpan(0, units, 0);
            }
            return new TimeSpan(0, 0, units);
        }
    }
}
