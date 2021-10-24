﻿using Formacion.Enums;
using Formacion.Extensions;
using Formacion.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Formacion.Calculators
{
    public class CalculatorNextExecutionTimeMonthlyEvery : CalculatorNextExecutionTimeMonthly
    {
        
        public CalculatorNextExecutionTimeMonthlyEvery(SchedulerConfig TheConfig):base(TheConfig)
        {
            
        }

        public override DateTime CalculateNextDate(DateTime CurrentDate)
        {
            
            DateTime DateInitCalc = this.GetFirstDayInitCalculo();
            DateTime DateCalculate = CurrentDate < DateInitCalc ? DateInitCalc : CurrentDate;
            while ((DateCalculate = this.CalculateDayInMonth(this.CalculateNextMonth(DateInitCalc,
              DateCalculate), DateCalculate)) <= CurrentDate)
            {
                DateCalculate = DateCalculate.AddDays(1).Date;
            }
            return DateCalculate;
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
            DateTime DateInitCalc = this.GetDayInMonth(this.config.StartDate);
            if (DateInitCalc < this.config.StartDate)
            {
                DateInitCalc = this.GetDayInMonth(new DateTime(this.config.StartDate.Year, this.config.StartDate.Month, 1).AddMonths(1));
            }
            return DateInitCalc;
        }

        private DateTime GetFirstDayInitCalculoWeekDay()
        {
            DateTime DateCalc = new DateTime(this.config.StartDate.Year, this.config.StartDate.Month, 1);
            if(DateCalc.GetIndexDayWeek() >= 5)
            {
                return DateCalc.AddDays(7 - DateCalc.GetIndexDayWeek());
            }
          
            return DateCalc;
        }

       
        private DateTime GetFirstDayInitCalculoWeekEndDay()
        {
            DateTime DateCalc = new DateTime(this.config.StartDate.Year, this.config.StartDate.Month, 1);
            if(DateCalc.GetIndexDayWeek() < 5 )
            {
                DateCalc.AddMonths(5 - DateCalc.GetIndexDayWeek());
            }
            return DateCalc;
        }

        protected override DateTime SetDayMonth(DateTime Date)
        {
            if(this.config.Monthly.TypesEvery == TypesEveryMonthly.Last)
            {
                return this.GetLastDayMonth(Date);
            }
           return this.GetDayInMonth(Date);
        }
        private DateTime GetDayInMonth(DateTime DateCalc)
        {
            if(this.config.Monthly.TypesDayEvery == TypesEveryDayMonthly.Weekday)
            {
                return GetDayInMonthDayOfWeek(DateCalc);
            }
            if(this.config.Monthly.TypesDayEvery == TypesEveryDayMonthly.Weekend)
            {
                return this.GetDayInMonthDayOfWeekend(DateCalc);
            }
            return this.GetDayInMonthAnyDayOfWeek(DateCalc);
           
        }

        private DateTime GetDayInMonthAnyDayOfWeek(DateTime DateCalc)
        {
            DateTime FirstDayMonth = new DateTime(DateCalc.Year, DateCalc.Month, 1);
            int Index = FirstDayMonth.GetIndexDayWeek();
            int Day = this.GetIndexDayWeekConfig();
            if (Index <= Day)
            {
                FirstDayMonth = FirstDayMonth.AddDays(Day - Index);
            }
            else
            {
                FirstDayMonth = FirstDayMonth.AddDays(7 - Index + Day);
            }
            if (this.HasCalculateWeek())
            {
                FirstDayMonth = this.GetNextWeek(FirstDayMonth, DateCalc);
            }
            return FirstDayMonth;
        }

        private DateTime GetDayInMonthDayOfWeek(DateTime DateCalc)
        {
            DateTime FirstDayMonth = new DateTime(DateCalc.Year, DateCalc.Month, 1);
            int Index = FirstDayMonth.GetIndexDayWeek();

            if ((Index + (int)this.config.Monthly.TypesEvery) > 4)
            {
                FirstDayMonth = FirstDayMonth.AddDays((4 - Index) + 2 + (((int)this.config.Monthly.TypesEvery) - (4 - Index)));
            }
            else
            {
                FirstDayMonth = FirstDayMonth.AddDays((int)this.config.Monthly.TypesEvery);
            }
            return FirstDayMonth;
        }

        private DateTime GetDayInMonthDayOfWeekend(DateTime DateCalc)
        {
            DateTime FirstDayMonth = new DateTime(DateCalc.Year, DateCalc.Month, 1);
            return FirstDayMonth.AddDays((5 + (int)this.config.Monthly.TypesEvery) - FirstDayMonth.GetIndexDayWeek());
        }

        private DateTime GetLastDayMonth(DateTime DateCalc)
        {
            DateTime LastDayMonth = new DateTime(DateCalc.Year, DateCalc.Month, DateTime.DaysInMonth(DateCalc.Year, DateCalc.Month));
            
            if(this.config.Monthly.TypesDayEvery == TypesEveryDayMonthly.Weekday)
            {
                return this.GetLastDayMonthWeekday(LastDayMonth);
            }
            if(this.config.Monthly.TypesDayEvery == TypesEveryDayMonthly.Weekend)
            {
                return this.GetLastDayMonthWeekEndDay(LastDayMonth); 
            }
            return this.GetLastDayMonthAnyDay(LastDayMonth);
        }

        private DateTime GetLastDayMonthWeekday(DateTime DateCalculate)
        {
            int Indice = DateCalculate.GetIndexDayWeek();
            if (Indice > 4)
            {
                DateCalculate = DateCalculate.AddDays(-(Indice - 4));
            }
            return DateCalculate;
        }
        private DateTime GetLastDayMonthWeekEndDay(DateTime DateCalculate)
        {
            int Indice = DateCalculate.GetIndexDayWeek();
            if (Indice < 5)
            {
                DateCalculate = DateCalculate.AddDays(-(Indice + 1));
            }
            return DateCalculate;
        }
        private DateTime GetLastDayMonthAnyDay(DateTime DateCalculate)
        {
            while(DateCalculate.GetIndexDayWeek() > (int)this.config.Monthly.TypesDayEvery)
            {
                DateCalculate = DateCalculate.AddDays(-1);  
            }
            return DateCalculate;
        }

        private DateTime GetNextWeek(DateTime DateToCalc, DateTime DateCalculated)
        {
            DateToCalc = DateToCalc.AddDays((7 * (int)this.config.Monthly.TypesEvery));
          
            return DateToCalc;
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
