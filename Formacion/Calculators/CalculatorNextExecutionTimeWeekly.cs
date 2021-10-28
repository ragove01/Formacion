using Formacion.Configs;
using Formacion.Extensions;
using Formacion.TextsTranslations;
using Formacion.Views;
using System;

namespace Formacion.Calculators
{
    public class CalculatorNextExecutionTimeWeekly
    {
        private readonly ConfigWeekly config;
        private readonly CalculatorNextExecutionTimeDailyFrecuency calculatorNextExecutionTimeDailyFrecuency;
        private readonly DateTime startDate;
        private readonly DateTime endDate;
        public CalculatorNextExecutionTimeWeekly(SchedulerConfig config)
        {
            this.config = config.Weekly;
            this.calculatorNextExecutionTimeDailyFrecuency = new CalculatorNextExecutionTimeDailyFrecuency(config.DailyFrecuenci);
            this.startDate = config.StartDate;
            this.endDate = CalculatorLastDateTimeCalc.GetLastDateTime(config);  
        }

     
        public DateTime CalculateNextDate(DateTime currentDate)
        {
            DateTime dateCalculate = this.GetNextDayTime(currentDate);
            while(dateCalculate == currentDate)
            {
                dateCalculate = this.GetNextDayTime(dateCalculate.AddDays(1).Date);
            }
            return dateCalculate;
        }

        private DateTime GetNextDayTime(DateTime currentDate)
        {
            DateTime firstDayWeek = this.GetNextWeekToCalculate(currentDate);
            DateTime? dateCalculated = this.GetNextDayInWeek(firstDayWeek > currentDate ? firstDayWeek : currentDate);
            if (dateCalculated == null)
            {
                firstDayWeek = firstDayWeek.AddDays(this.config.NumberDaysEvery);
                dateCalculated = this.GetNextDayInWeek(firstDayWeek);
                if (dateCalculated == null)
                {
                    throw new ApplicationException(Translator.GetText(TextsIndex.NotNextExecution));
                }
            }
            return this.calculatorNextExecutionTimeDailyFrecuency.GetNextTime(dateCalculated.Value);
        }
        private DateTime GetNextWeekToCalculate(DateTime currentDate)
        {
            DateTime startDateWeek = this.GetDateInitCalculo();
            if (currentDate < this.startDate)
            {
                return this.startDate;
            }
            return GetFirstDayNextWeek(startDateWeek, currentDate);
            
        }

        private DateTime GetFirstDayNextWeek(DateTime startDayCalculate, DateTime currentDate)
        {
            int days = (currentDate - startDayCalculate).Days;
            int rest = 0;
            days = Math.DivRem(days, this.config.NumberDaysEvery, out rest);
            DateTime nextDate = startDayCalculate.AddDays(days * this.config.NumberDaysEvery);
            if (rest > 6)
            {
                nextDate = nextDate.AddDays(this.config.NumberDaysEvery);
            }
            return nextDate;
        }
     

        private DateTime? GetNextDayInWeek(DateTime currentDate)
        {
            if(this.endDate < currentDate)
            {
                return null;
            }
            int startIndex = currentDate.GetIndexDayWeek();
            for (int index = startIndex; index < 7; index++)
            {
                if (this.config.SelectedDays[index])
                {
                    if(index == startIndex)
                    {
                        return currentDate;
                    }
                    return currentDate.AddDays(index - startIndex).Date;
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

    }
}
