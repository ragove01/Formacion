using System;
using Formacion.Enums;
using Formacion.Configs;
using Formacion.Views;

namespace Formacion.Calculators
{
    public class CalculatorNextExecutionTime
    {
        private readonly SchedulerConfig config;
        private readonly ConfigDailyFrecuency configDailyFrecuenci;
        private readonly ConfigWeekly configWeekly;
        private readonly ConfigMonthly configMontly;
        
        

       

        public CalculatorNextExecutionTime(SchedulerConfig config)
        {
            this.config = config;
            this.configDailyFrecuenci = config.DailyFrecuenci??new ConfigDailyFrecuency() { Frecuenci = TypesOccursDailyFrecuency.Once };
            this.configWeekly = this.config.Weekly;
            this.configMontly = this.config.Monthly;
            
          
        }
  
        public DateTime GetNext(DateTime dateCalc)
        {

            if (this.configWeekly == null && this.configMontly == null)
            {
                return this.GetNextDateNotWeeklyConfiguration(dateCalc);
            }
            if (this.configWeekly != null)
            {

                return this.CalculateNextDateTimeOnce(dateCalc);
            }
            return this.CalculateNextDateTimeMonthly(dateCalc);


        }


        private DateTime GetNextDateNotWeeklyConfiguration(DateTime dateCalc)
        {
            DateTime nextDate = new CalculatorNextExecutionTimeDailyFrecuency(this.configDailyFrecuenci).GetNextTime(dateCalc);
            if(nextDate > dateCalc)
            {
                return nextDate;
            }
            if (this.config.Occurs == TypesOccurs.Weekly)
            {
                return GetTimesDate(dateCalc.AddDays(7).Date);
            }
            if (this.config.Occurs == TypesOccurs.Monthly)
            {
                return GetTimesDate(dateCalc.AddMonths(1).Date);
            }
            return GetTimesDate(dateCalc.AddDays(1).Date);
        }

        private DateTime GetTimesDate(DateTime dayCalc)
        {
            return new CalculatorNextExecutionTimeDailyFrecuency(this.configDailyFrecuenci).GetNextTime(dayCalc);
           
        }

        private DateTime CalculateNextDateTimeOnce(DateTime day)
        {
            return new CalculatorNextExecutionTimeWeekly(this.config).CalculateNextDate(day);
            

        }

        private DateTime CalculateNextDateTimeMonthly(DateTime day)
        {
            if (this.configMontly.Type == TypesMontlyFrecuency.Day)
            {
                return new CalculatorNextExecutionTimeMonthlyDay(this.config).CalculateNextDate(day);
            }

            return new CalculatorNextExecutionTimeMonthlyEvery(this.config).CalculateNextDate(day);
        }
   
    }
}
;