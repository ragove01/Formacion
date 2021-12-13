using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formacion.Data.Models.Scheluder
{
    public class SchedulerConfig
    {
        public int SchedulerConfigId { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public int Type { get; set; }
        public int Occurs { get; set; }
        public int NumberOccurs { get; set; }
        public DateTime? DateTime { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public virtual SchedulerDailyConfig DailyConfig{get;set;}
        public virtual SchedulerWeeklyConfig WeeklyConfig { get;set;}
        public virtual SchedulerMonthlyConfig MonthlyConfig{get;set;}
    
    }
}
