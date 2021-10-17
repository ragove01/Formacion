using Formacion.Enums;
using System;

namespace Formacion.Configs
{
    public class ConfigDailyFrecuency
    {

        public TypesOccursDailyFrecuency Frecuenci { get; set; }
        public TimeSpan? OnceTime { get; set; }
        public TypesUnitsDailyFrecuency TypeUnit { get; set; }

        public int NumberOccurs { get; set; }

        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
    }
}
