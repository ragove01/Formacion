using Formacion.Interfaces;
using System;
using Formacion.Enums;
using Formacion.Configs;
using Formacion.Extensions;
using System.Collections.Generic;
using Formacion.Views;

namespace Formacion.Calculators
{
    public class CalculatorNextExecutionTime
    {
        private readonly SchedulerConfig config;
        private readonly ConfigDailyFrecuency configDailyFrecuenci;
        private readonly ConfigWeekly configWeekly;
        
        

       

        public CalculatorNextExecutionTime(SchedulerConfig TheConfig)
        {
            this.config = TheConfig;
            this.configDailyFrecuenci = TheConfig.DailyFrecuenci??new ConfigDailyFrecuency() { Frecuenci = TypesOccursDailyFrecuency.Once };
            this.configWeekly = config.Weekly;
            
          
        }
  
        public DateTime GetNext(DateTime dateCalc)
        {

            if (this.configWeekly == null)
            {
                return this.GetNextDateNotWeeklyConfiguration(dateCalc);
            }

            return this.CalculateNextDateTimeOnce(dateCalc);


        }


        private DateTime GetNextDateNotWeeklyConfiguration(DateTime dateCalc)
        {
            DateTime NextDate = new CalculatorNextExecutionTimeDailyFrecuency(this.configDailyFrecuenci).GetNextTime(dateCalc);
            if(NextDate > dateCalc)
            {
                return NextDate;
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

      

        

    }
}
;