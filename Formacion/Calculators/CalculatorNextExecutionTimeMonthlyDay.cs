using Formacion.Enums;
using Formacion.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Formacion.Calculators
{
    public class CalculatorNextExecutionTimeMonthlyDay:CalculatorNextExecutionTimeMonthly
    {
        public CalculatorNextExecutionTimeMonthlyDay(SchedulerConfig TheConfig):base(TheConfig)
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
            if (this.config.StartDate.Day > this.config.Monthly.DayMonth)
            {
                return this.SetDayMonth(new DateTime(this.config.StartDate.Year, this.config.StartDate.Month, 1)
                        .AddMonths(1));
            }
            return this.SetDayMonth(new DateTime(this.config.StartDate.Year, this.config.StartDate.Month, 1));
        }

        protected override DateTime SetDayMonth(DateTime Date)
        {
            if (DateTime.DaysInMonth(Date.Year, Date.Month) < this.config.Monthly.DayMonth)
            {
                return new DateTime(Date.Year, Date.Month, DateTime.DaysInMonth(Date.Year, Date.Month)).AddDays(1);
            }
            return new DateTime(Date.Year, Date.Month, this.config.Monthly.DayMonth.Value);
        }
    }
}
