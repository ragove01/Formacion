using Formacion.Configs;
using Formacion.Enums;
using System;
using Formacion.Extensions;
namespace Formacion.Calculators
{
    public class CalculatorNextExecutionTimeDailyFrecuency
    {
        private readonly ConfigDailyFrecuency configDailyFrecuenci;
        
        public CalculatorNextExecutionTimeDailyFrecuency(ConfigDailyFrecuency configDailyFrecuenci)
        {
            if(configDailyFrecuenci == null)
            {
                configDailyFrecuenci = new ConfigDailyFrecuency()
                {
                    Frecuenci = TypesOccursDailyFrecuency.Once
                };
            }
            this.configDailyFrecuenci = configDailyFrecuenci;
            
        }

      

        private DateTime GetNextOnce(DateTime day)
        {
            return day.SetTimeToDate(this.configDailyFrecuenci.OnceTime.GetValueOrDefault());
        }
        private DateTime GetNextFrecuenly(DateTime date)
        {
            DateTime startDate = date.SetTimeToDate(this.configDailyFrecuenci.StartTime.Value);
            DateTime endTime = date.SetTimeToDate(this.configDailyFrecuenci.EndTime.Value);
            while (startDate <= endTime)
            {
                if (startDate > date)
                {
                    return startDate;
                }
                startDate = startDate.AddInteval(this.configDailyFrecuenci.TypeUnit, this.configDailyFrecuenci.NumberOccurs);
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
