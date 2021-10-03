using System;
using Formacion.Enums;
using Formacion.Interfaces;

namespace Formacion.Views
{
    public class SchedulerConfig : IConfig, IConfigOnce, IConfigRecurring, ILimits
    {
        public TypesSchedule Type { get; set; }
        public bool Active { get; set; }
        public DateTime? DateTime { get; set; }
        public TypesOccurs Occurs { get; set; }
        public int NumberOccurs { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

    }
}
