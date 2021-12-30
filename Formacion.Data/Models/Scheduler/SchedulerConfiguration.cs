using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Formacion.Data.Models.Scheduler
{
    [Table("SchedulerConfiguration")]
    public partial class SchedulerConfiguration
    {
        [Key]
        public int SchedulerId { get; set; }
        public bool Active { get; set; }
        public int Type { get; set; }
        public int Occurs { get; set; }
        public int NumberOccurs { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? DateTime { get; set; }
        public DateTime? DateTimeNextExecution { get; set; }
        public short? Frecuency { get; set; }
        public TimeSpan? OneTime { get; set; }
        public short? TypeUnit { get; set; }
        public int? NumberOccursDailyConfiguration { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public short? TypeMonthlyConfiguration { get; set; }
        public short? DayMonth { get; set; }
        public short? EveryNumberMonths { get; set; }
        public short? TypesEvery { get; set; }
        public short? TypesDayEvery { get; set; }
        public short? Every { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
    }
}
