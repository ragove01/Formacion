using System;
using Formacion.Configs;
using Formacion.Enums;

namespace Formacion.Views
{
    public class SchedulerConfig 
    {
        public TypesSchedule Type { get; set; }
        public bool Active { get; set; }
        public DateTime? DateTime { get; set; }
        public TypesOccurs Occurs { get; set; }
        public int NumberOccurs { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public ConfigDailyFrecuency DailyFrecuenci { get; set; }
        public ConfigWeekly Weekly { get; set; }
        public ConfigMontly Monthly { get; set; }

    }
}
