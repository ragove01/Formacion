using Formacion.Calculators;
using Formacion.Enums;
using Formacion.Instantiators;
using Formacion.Interfaces;
using Formacion.Views;
using System;
using FluentAssertions;
using Xunit;
using System.Linq;

namespace Testing
{
    public class TestCalculator
    {
        [Fact]
        public void Test_Calculator_Once()
        {
            SchedulerConfig TheConfig = new SchedulerConfig();
            var Calculator = InstantiatorCalculator.GetCalculator(TypesSchedule.Once);
            Assert.IsType<CalculatorOnce>(Calculator);
            TheConfig.DateTime = new DateTime(2020, 1, 8, 14, 0, 0);
            TheConfig.StartDate = new DateTime(2020, 1, 1);
            TheConfig.Active = true;
            var Result = Calculator.Calulate(new DateTime(2020,1,4),TheConfig,TheConfig);
            Assert.NotNull(Result);
            Assert.Equal(1, Result.Count);
            Assert.Equal(TheConfig.DateTime, Result.FirstOrDefault()?.NextExecution);
            Assert.Equal(TheConfig.StartDate, Result.FirstOrDefault()?.StartDate);

        }

        [Fact]
        public void Test_Calculator_Recurring()
        {
            SchedulerConfig TheConfig = new SchedulerConfig();
            var Calculator = InstantiatorCalculator.GetCalculator(TypesSchedule.Recurring);
            Assert.IsType<CalculatorRecurring>(Calculator);
            TheConfig.Type = TypesSchedule.Recurring;

            TheConfig.StartDate = new DateTime(2020, 1, 1);
            TheConfig.Active = true;
            TheConfig.NumberOccurs = 1;
            var Result = Calculator.Calulate(new DateTime(2020, 1, 4), TheConfig, TheConfig);
            Assert.NotNull(Result);
            Assert.Equal(1, Result.Count);
            Assert.Equal(new DateTime(2020, 1, 5), Result.FirstOrDefault()?.NextExecution);
            Assert.Equal(TheConfig.StartDate, Result.FirstOrDefault()?.StartDate);
            TheConfig.NumberOccurs = 5;
            Result = Calculator.Calulate(new DateTime(2020, 1, 4), TheConfig, TheConfig);
            Assert.NotNull(Result);
            Assert.Equal(5, Result.Count);
        }
    }
}
