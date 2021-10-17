using Formacion.Enums;
using Formacion.Views;
using System;

namespace Formacion.Calculators
{
    public class CalculatorNextExecutionTimeMonthly
    {
        protected readonly SchedulerConfig config;
        protected CalculatorNextExecutionTimeMonthly(SchedulerConfig TheConfig)
        {
            this.config = TheConfig;
        }

        public virtual DateTime CalculateNextDate(DateTime CurrentDate)
        {
            throw new NotImplementedException();
        }

       
        protected DateTime CalculateDayInMonth(DateTime DateMonth, DateTime CurrentDate)
        {
            DateTime NextDate;
            while ((NextDate = this.SetDayMonth(DateMonth)) < CurrentDate.Date)
            {
                DateMonth = DateMonth.AddMonths(this.config.Monthly.EveryNumberMonths);
            }
            return this.SetHourOfDay(NextDate<CurrentDate?CurrentDate:NextDate);
        }

        protected DateTime CalculateNextMonth(DateTime DateInitCalc, DateTime CurrentDate)
        {
           
            int Months = this.GetDiffMonth(DateInitCalc,CurrentDate);
            if(Months == 0)
            {
                return new DateTime(DateInitCalc.Year, DateInitCalc.Month, 1);
            }
            int Rest = 0;
            Months = Math.DivRem(Months, this.config.Monthly.EveryNumberMonths, out Rest);
            DateTime NextDate = new DateTime(DateInitCalc.Year,DateInitCalc.Month,1).AddMonths(Months * this.config.Monthly.EveryNumberMonths);
            if (Rest > 0)
            {
                NextDate = NextDate.AddMonths(this.config.Monthly.EveryNumberMonths);
            }
            return NextDate;
        }

        protected int GetDiffMonth(DateTime DateIni, DateTime DateFin)
        {
            if(DateIni.Year == DateFin.Year)
            {
                return DateFin.Month - DateIni.Month;  
            }
            int NumberMonth = ((DateFin.Year - DateIni.Year) * 12) + DateFin.Month - DateIni.Month;
            if(DateFin.Day < DateIni.Day)
            {
                NumberMonth--; 
            }
            return NumberMonth; 
        }
        private DateTime GetFirstDayInitCalculo()
        {
            if(this.config.StartDate.Day > this.config.Monthly.DayMonth)
            {
                return this.SetDayMonth(new DateTime(this.config.StartDate.Year, this.config.StartDate.Month, 1)
                        .AddMonths(1));  
            }
            return this.SetDayMonth(new DateTime(this.config.StartDate.Year, this.config.StartDate.Month, 1));
        }

        protected virtual DateTime SetDayMonth(DateTime Date)
        {
            throw new NotImplementedException();
        }
        private DateTime SetHourOfDay(DateTime Date)
        {
            if(this.config.DailyFrecuenci == null)
            {
                return Date;
            }
            return new CalculatorNextExecutionTimeDailyFrecuency(this.config.DailyFrecuenci).GetNextTime(Date);   
        }
    }
}
