using Formacion.Instantiators;
using Formacion.Formatters;
using Formacion.Views;
using Formacion;
using System;
using Xunit;
using Formacion.Enums;

namespace Testing
{
    public class TestFormatter
    {
        [Fact]
        public void Test_Validator_Formatter_Once()
        {
            SchedulerConfig LaConfiguracion = new SchedulerConfig();
            var Formatter = InstantiatorFormatter.GetFormatter(LaConfiguracion, LaConfiguracion);
            Assert.IsType<FormatterOnce>(Formatter);
            Result TheResult = new Result(new DateTime(2020, 1, 1), new DateTime(2020, 1, 4, 14, 0, 0));
            Assert.Equal("Occurs once. Schedule will be used on 04/01/2020 at 14:00 starting on 01/01/2020",
                Formatter.Formatter(TheResult));
        }

        [Fact]
        public void Test_Validator_Formatter_Recurring()
        {
            SchedulerConfig LaConfiguracion = new SchedulerConfig();
            LaConfiguracion.Type = TypesSchedule.Recurring;
            LaConfiguracion.NumberOccurs = 1;
            var Formatter = InstantiatorFormatter.GetFormatter(LaConfiguracion, LaConfiguracion);
            Assert.IsType<FormatterRecurring>(Formatter);
            Result TheResult = new Result(new DateTime(2020, 1, 1), new DateTime(2020, 1, 4, 14, 0, 0));
            Assert.Equal("Occurs every daily. Schedule will be used on 04/01/2020 at 14:00 starting on 01/01/2020",
                Formatter.Formatter(TheResult));
            
        }
    }
}
