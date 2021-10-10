using Formacion.Configs;
using Formacion.Enums;
using System;
using Formacion.Extensions;
namespace Formacion.Calculators
{
    public class CalculatorNextExecutionTimeDailyFrecuency
    {
        private readonly ConfigDailyFrecuency configDailyFrecuenci;
        
        public CalculatorNextExecutionTimeDailyFrecuency(ConfigDailyFrecuency TheConfigDailyFrecuenci)
        {
            if(TheConfigDailyFrecuenci == null)
            {
                TheConfigDailyFrecuenci = new ConfigDailyFrecuency()
                {
                    Frecuenci = TypesOccursDailyFrecuency.Once
                };
            }
            this.configDailyFrecuenci = TheConfigDailyFrecuenci;
            
        }

      

        private DateTime GetNextOnce(DateTime day)
        {
            return day.SetTimeToDate(this.configDailyFrecuenci.OnceTime.GetValueOrDefault());
        }
        private DateTime GetNextFrecuenly(DateTime date)
        {
            DateTime StartDate = date.SetTimeToDate(this.configDailyFrecuenci.StartTime.Value);
            DateTime EndTime = date.SetTimeToDate(this.configDailyFrecuenci.EndTime.Value);
            while (StartDate <= EndTime)
            {
                if (StartDate > date)
                {
                    return StartDate;
                }
                StartDate = StartDate.AddInteval(this.configDailyFrecuenci.TypeUnit, this.configDailyFrecuenci.NumberOccurs);
            }

            return date;
        }


        public DateTime GetNextTime(DateTime date)
        {
            if (this.configDailyFrecuenci.Frecuenci == TypesOccursDailyFrecuency.Once)
            {
                return this.GetNextOnce(date);
            }

            return this.GetNextFrecuenly(date); 

        }
    }
}
