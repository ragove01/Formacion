using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formacion.Data.Models.Scheluder
{
	public class SchedulerDailyConfig
	{
		public int SchedulerDailyConfigId { get; set; }
		
		public Int16 Frecuency { get; set; }
		public TimeSpan? OneTime { get; set; }
		public Int16? TypeUnit { get; set; }
		public int NumberOccurs { get; set; }
		public TimeSpan? StartTime { get; set; }
		public TimeSpan? EndTime { get; set; }

		public int SchedulerConfigId { get; set; }
		public SchedulerConfig Config { get; set; }
	}
}
