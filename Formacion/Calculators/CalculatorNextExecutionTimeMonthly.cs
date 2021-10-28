using Formacion.Enums;
using Formacion.Views;
using System;

namespace Formacion.Calculators
{
    public abstract class CalculatorNextExecutionTimeMonthly
    {
        protected readonly SchedulerConfig config;
        protected CalculatorNextExecutionTimeMonthly(SchedulerConfig configArgs)
        {
            this.config = configArgs;
        }

        public abstract DateTime CalculateNextDate(DateTime currentDate);
       

       
        protected DateTime CalculateDayInMonth(DateTime dateMonth, DateTime currentDate)
        {
            DateTime nextDate;
            while ((nextDate = this.SetDayMonth(dateMonth)) < currentDate.Date)
            {
                dateMonth = dateMonth.AddMonths(this.config.Monthly.EveryNumberMonths);
            }
            return this.SetHourOfDay(nextDate<currentDate?currentDate:nextDate);
        }

        protected DateTime CalculateNextMonth(DateTime dateInitCalc, DateTime currentDate)
        {
           
            int months = this.GetDiffMonth(dateInitCalc,currentDate);
            if(months == 0)
            {
                return new DateTime(dateInitCalc.Year, dateInitCalc.Month, 1);
            }
            int rest = 0;
            months = Math.DivRem(months, this.config.Monthly.EveryNumberMonths, out rest);
            DateTime nextDate = new DateTime(dateInitCalc.Year,dateInitCalc.Month,1).AddMonths(months * this.config.Monthly.EveryNumberMonths);
            if (rest > 0)
            {
                nextDate = nextDate.AddMonths(this.config.Monthly.EveryNumberMonths);
            }
            return nextDate;
        }

        protected int GetDiffMonth(DateTime dateIni, DateTime dateFin)
        {
            if(dateIni.Year == dateFin.Year)
            {
                return dateFin.Month - dateIni.Month;  
            }
            int numberMonth = ((dateFin.Year - dateIni.Year) * 12) + dateFin.Month - dateIni.Month;
            if(dateFin.Day < dateIni.Day)
            {
                numberMonth--; 
            }
            return numberMonth; 
        }

        protected abstract DateTime SetDayMonth(DateTime date);
        
        private DateTime SetHourOfDay(DateTime date)
        {
            if(this.config.DailyFrecuenci == null)
            {
                return date;
            }
            return new CalculatorNextExecutionTimeDailyFrecuency(this.config.DailyFrecuenci).GetNextTime(date);   
        }
    }
}
