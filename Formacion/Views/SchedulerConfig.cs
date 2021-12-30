using System;
using System.Globalization;
using Formacion.Configs;
using Formacion.Enums;

namespace Formacion.Views
{
    public class SchedulerConfig 
    {
        public SchedulerConfig()
        {
            this.Culture = CultureInfo.CurrentCulture;
        }
        public int? SchedulerId { get; set; }
        public string Name { get; set; }
        public TypesSchedule Type { get; set; }
        public bool Active { get; set; }
        public DateTime? DateTime { get; set; }
        public TypesOccurs Occurs { get; set; }
        public int NumberOccurs { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? DateTimeNextExecution { get; set; }
        public ConfigDailyFrecuency DailyFrecuenci { get; set; }
        public ConfigWeekly Weekly { get; set; }
        public ConfigMonthly Monthly { get; set; }
        public CultureInfo Culture { get; set; }
        

    }
}
