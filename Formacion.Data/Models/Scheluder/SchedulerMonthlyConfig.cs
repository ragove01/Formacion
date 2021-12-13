using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formacion.Data.Models.Scheluder
{
    public class SchedulerMonthlyConfig
    {
        public int SchedulerMonthlyConfigId { get;set; }
        public int SchedulerConfigId { get; set; }
        public short Type { get; set; }
        public short? DayMonth { get; set; }
        public short EveryNumberMonths { get; set; }
        public short? TypesEvery { get; set; }
        public short? TypesDayEvery { get; set; }
        public SchedulerConfig Config { get; set; }
    }
}
