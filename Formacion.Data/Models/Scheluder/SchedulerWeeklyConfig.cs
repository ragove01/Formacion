using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formacion.Data.Models.Scheluder
{
	public class SchedulerWeeklyConfig
	{
		public int SchedulerWeeklyConfigId { get; set; }
		public int SchedulerConfigId { get; set; }
		public Int16 Every { get; set; }
		public bool Monday { get; set; }
		public bool Tuesday { get; set; }
		public bool Wednesday { get; set; }
		public bool Thursday { get; set; }
		public bool Friday { get; set; }
		public bool Saturday { get; set; }
		public bool Sunday { get; set; }
		public SchedulerConfig Config{get;set;} 

	}
}
