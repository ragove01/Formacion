using Formacion.Configs;
using Formacion.Views;
using System;

namespace Formacion.Calculators
{
    public class CalculatorNextExecutionTimeWeekly
    {
        private readonly ConfigWeekly config;
        private readonly CalculatorNextExecutionTimeDailyFrecuency calculatorNextExecutionTimeDailyFrecuency;
        private readonly DateTime startDate;


        public CalculatorNextExecutionTimeWeekly(SchedulerConfig TheConfig)
        {
            this.config = TheConfig.Weekly;
            this.calculatorNextExecutionTimeDailyFrecuency = new CalculatorNextExecutionTimeDailyFrecuency(TheConfig.DailyFrecuenci);
            this.startDate = TheConfig.StartDate;
        }

     
        public DateTime CalculateNextDate(DateTime CurrentDate)
        {
            DateTime DateCalculate = this.GetNextDayTime(CurrentDate);
            while(DateCalculate == CurrentDate)
            {
                DateCalculate = this.GetNextDayTime(DateCalculate.AddDays(1).Date);
            }
            return DateCalculate;
        }

        private DateTime GetNextDayTime(DateTime CurrentDate)
        {
            DateTime FirstDayWeek = this.GetNextWeekToCalculate(CurrentDate);
            DateTime? DateCalculated = this.GetNextDayInWeek(FirstDayWeek > CurrentDate ? FirstDayWeek : CurrentDate);
            if (DateCalculated == null)
            {
                FirstDayWeek = FirstDayWeek.AddDays(this.config.NumberDaysEvery);
                DateCalculated = this.GetNextDayInWeek(FirstDayWeek);
                if (DateCalculated == null)
                {
                    throw new ApplicationException("Not next execution time");
                }
            }
            return this.calculatorNextExecutionTimeDailyFrecuency.GetNextTime(DateCalculated.Value);
        }
        private DateTime GetNextWeekToCalculate(DateTime CurrentDate)
        {
            DateTime StartDateWeek = this.GetDateInitCalculo();
            if (CurrentDate < this.startDate)
            {
                return this.startDate;
            }
            int Days = (CurrentDate - StartDateWeek).Days;
            int Rest = 0;
            Days = Math.DivRem(Days, this.config.NumberDaysEvery, out Rest);
            DateTime NextDate = StartDateWeek.AddDays(Days * this.config.NumberDaysEvery);
            if (Rest > 6)
            {
                NextDate = NextDate.AddDays(this.config.NumberDaysEvery);
            }
            return NextDate;
        }
     

        private DateTime? GetNextDayInWeek(DateTime currentDate)
        {
            int StartIndex = this.GetIndexDay(currentDate);
            for (int Index = StartIndex; Index < 7; Index++)
            {
                if (this.config.SelectedDays[Index])
                {
                    if(Index == StartIndex)
                    {
                        return currentDate;
                    }
                    return currentDate.AddDays(Index - StartIndex).Date;
                }
            }
            return null;
        }

        private DateTime GetDateInitCalculo()
        {
            if(this.startDate.DayOfWeek == DayOfWeek.Sunday)
            {
                return this.startDate.AddDays(-6);
            }
            return this.startDate.AddDays((((int)startDate.DayOfWeek) - 1) * -1); 
        }
               
        private int GetIndexDay(DateTime dateTime)
        {
            if(dateTime.DayOfWeek == DayOfWeek.Sunday)
            {
                return 6;
            }
            return (int)dateTime.DayOfWeek - 1;
        }
        
   
    }
}
