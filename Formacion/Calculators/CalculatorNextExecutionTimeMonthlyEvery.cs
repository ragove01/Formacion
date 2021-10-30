using Formacion.Enums;
using Formacion.Extensions;
using Formacion.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Formacion.Calculators
{
    public class CalculatorNextExecutionTimeMonthlyEvery : CalculatorNextExecutionTimeMonthly
    {
        
        public CalculatorNextExecutionTimeMonthlyEvery(SchedulerConfig config):base(config)
        {
            
        }

        public override DateTime CalculateNextDate(DateTime currentDate)
        {
            
            DateTime dateInitCalc = this.GetFirstDayInitCalculo();
            DateTime dateCalculate = currentDate < dateInitCalc ? dateInitCalc : currentDate;
            while ((dateCalculate = this.CalculateDayInMonth(this.CalculateNextMonth(dateInitCalc,
              dateCalculate), dateCalculate)) <= currentDate)
            {
                dateCalculate = dateCalculate.AddDays(1).Date;
            }
            return dateCalculate;
        }

        private DateTime GetFirstDayInitCalculo()
        {
            if(this.config.Monthly.TypesDayEvery == TypesEveryDayMonthly.Weekday)
            {
                return this.GetFirstDayInitCalculoWeekDay();

            }
            if(this.config.Monthly.TypesDayEvery == TypesEveryDayMonthly.Weekend)
            {
                return this.GetFirstDayInitCalculoWeekEndDay();
            }
            return this.GetFirstDayInitCalculoWeekAnyDay(); 
        }

        private DateTime GetFirstDayInitCalculoWeekAnyDay()
        {
            DateTime dateInitCalc = this.GetDayInMonth(this.config.StartDate);
            if (dateInitCalc < this.config.StartDate)
            {
                dateInitCalc = this.GetDayInMonth(new DateTime(this.config.StartDate.Year, this.config.StartDate.Month, 1).AddMonths(1));
            }
            return dateInitCalc;
        }

        private DateTime GetFirstDayInitCalculoWeekDay()
        {
            DateTime dateCalc = new DateTime(this.config.StartDate.Year, this.config.StartDate.Month, 1);
            if(dateCalc.GetIndexDayWeek() >= 5)
            {
                return dateCalc.AddDays(7 - dateCalc.GetIndexDayWeek());
            }
          
            return dateCalc;
        }

       
        private DateTime GetFirstDayInitCalculoWeekEndDay()
        {
            DateTime dateCalc = new DateTime(this.config.StartDate.Year, this.config.StartDate.Month, 1);
            if(dateCalc.GetIndexDayWeek() < 5 )
            {
                dateCalc.AddMonths(5 - dateCalc.GetIndexDayWeek());
            }
            return dateCalc;
        }

        protected override DateTime SetDayMonth(DateTime date)
        {
            if(this.config.Monthly.TypesEvery == TypesEveryMonthly.Last)
            {
                return this.GetLastDayMonth(date);
            }
           return this.GetDayInMonth(date);
        }
        private DateTime GetDayInMonth(DateTime dateCalc)
        {
            if(this.config.Monthly.TypesDayEvery == TypesEveryDayMonthly.Weekday)
            {
                return GetDayInMonthDayOfWeek(dateCalc);
            }
            if(this.config.Monthly.TypesDayEvery == TypesEveryDayMonthly.Weekend)
            {
                return this.GetDayInMonthDayOfWeekend(dateCalc);
            }
            return this.GetDayInMonthAnyDayOfWeek(dateCalc);
           
        }

        private DateTime GetDayInMonthAnyDayOfWeek(DateTime dateCalc)
        {
            DateTime firstDayMonth = new DateTime(dateCalc.Year, dateCalc.Month, 1);
            int index = firstDayMonth.GetIndexDayWeek();
            int day = this.GetIndexDayWeekConfig();
            if (index <= day)
            {
                firstDayMonth = firstDayMonth.AddDays(day - index);
            }
            else
            {
                firstDayMonth = firstDayMonth.AddDays(7 - index + day);
            }
            if (this.HasCalculateWeek())
            {
                firstDayMonth = this.GetNextWeek(firstDayMonth);
            }
            return firstDayMonth;
        }

        private DateTime GetDayInMonthDayOfWeek(DateTime dateCalc)
        {
            DateTime firstDayMonth = new DateTime(dateCalc.Year, dateCalc.Month, 1);
            int index = firstDayMonth.GetIndexDayWeek();

            if ((index + (int)this.config.Monthly.TypesEvery) > 4)
            {
                firstDayMonth = firstDayMonth.AddDays((4 - index) + 2 + (((int)this.config.Monthly.TypesEvery) - (4 - index)));
            }
            else
            {
                firstDayMonth = firstDayMonth.AddDays((int)this.config.Monthly.TypesEvery);
            }
            return firstDayMonth;
        }

        private DateTime GetDayInMonthDayOfWeekend(DateTime dateCalc)
        {
            DateTime firstDayMonth = new DateTime(dateCalc.Year, dateCalc.Month, 1);
            return firstDayMonth.AddDays((5 + (int)this.config.Monthly.TypesEvery) - firstDayMonth.GetIndexDayWeek());
        }

        private DateTime GetLastDayMonth(DateTime dateCalc)
        {
            DateTime lastDayMonth = new DateTime(dateCalc.Year, dateCalc.Month, DateTime.DaysInMonth(dateCalc.Year, dateCalc.Month));
            
            if(this.config.Monthly.TypesDayEvery == TypesEveryDayMonthly.Weekday)
            {
                return this.GetLastDayMonthWeekday(lastDayMonth);
            }
            if(this.config.Monthly.TypesDayEvery == TypesEveryDayMonthly.Weekend)
            {
                return this.GetLastDayMonthWeekEndDay(lastDayMonth); 
            }
            return this.GetLastDayMonthAnyDay(lastDayMonth);
        }

        private DateTime GetLastDayMonthWeekday(DateTime dateCalculate)
        {
            int index = dateCalculate.GetIndexDayWeek();
            if (index > 4)
            {
                dateCalculate = dateCalculate.AddDays(-(index - 4));
            }
            return dateCalculate;
        }
        private DateTime GetLastDayMonthWeekEndDay(DateTime dateCalculate)
        {
            int index = dateCalculate.GetIndexDayWeek();
            if (index < 5)
            {
                dateCalculate = dateCalculate.AddDays(-(index + 1));
            }
            return dateCalculate;
        }
        private DateTime GetLastDayMonthAnyDay(DateTime dateCalculate)
        {
            while(dateCalculate.GetIndexDayWeek() > (int)this.config.Monthly.TypesDayEvery)
            {
                dateCalculate = dateCalculate.AddDays(-1);  
            }
            return dateCalculate;
        }

        private DateTime GetNextWeek(DateTime dateToCalc)
        {
            dateToCalc = dateToCalc.AddDays((7 * (int)this.config.Monthly.TypesEvery));
          
            return dateToCalc;
        }

        private int GetIndexDayWeekConfig()
        {
           return (int)this.config.Monthly.TypesDayEvery;
        }

        private bool HasCalculateWeek()
        {
            return this.config.Monthly.TypesDayEvery == TypesEveryDayMonthly.Monday ||
                this.config.Monthly.TypesDayEvery == TypesEveryDayMonthly.Tuesday ||
                this.config.Monthly.TypesDayEvery == TypesEveryDayMonthly.Wednesday ||
                this.config.Monthly.TypesDayEvery == TypesEveryDayMonthly.Thursday ||
                this.config.Monthly.TypesDayEvery == TypesEveryDayMonthly.Friday ||
                this.config.Monthly.TypesDayEvery == TypesEveryDayMonthly.Saturday ||
                this.config.Monthly.TypesDayEvery == TypesEveryDayMonthly.Sunday;
        }
     }
}
