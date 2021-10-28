using Formacion.Enums;
using Formacion.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Formacion.Calculators
{
    public class CalculatorNextExecutionTimeMonthlyDay:CalculatorNextExecutionTimeMonthly
    {
        public CalculatorNextExecutionTimeMonthlyDay(SchedulerConfig config):base(config)
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
            if (this.config.StartDate.Day > this.config.Monthly.DayMonth)
            {
                return this.SetDayMonth(new DateTime(this.config.StartDate.Year, this.config.StartDate.Month, 1)
                        .AddMonths(1));
            }
            return this.SetDayMonth(new DateTime(this.config.StartDate.Year, this.config.StartDate.Month, 1));
        }

        protected override DateTime SetDayMonth(DateTime date)
        {
            if (DateTime.DaysInMonth(date.Year, date.Month) < this.config.Monthly.DayMonth)
            {
                return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month)).AddDays(1);
            }
            return new DateTime(date.Year, date.Month, this.config.Monthly.DayMonth.Value);
        }
    }
}
