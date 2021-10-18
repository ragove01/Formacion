using Formacion.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Formacion.Configs
{
    public class ConfigMonthly
    {
        public TypesMontlyFrecuency Type { get; set; }
        public int? DayMonth { get; set; }
        public int EveryNumberMonths { get; set; }
        public TypesEveryMonthly? TypesEvery { get; set; }
        public TypesEveryDayMonthly? TypesDayEvery { get; set; }


    }
}
