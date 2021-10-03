using Formacion.Enums;
using Formacion;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Formacion.Views;

namespace Testing
{
    public class TestGenerator
    {
        [Theory]
        [InlineData(TypesSchedule.Once,"08/01/2020 14:00:00")]
        [InlineData(TypesSchedule.Recurring,"05/01/2020 00:00:00")]
        public void Test_Generator(TypesSchedule TheType,string NextDateExpected)
        {
            var TheGenerator = new ScheluderGenerator();
            var Config = new SchedulerConfig();
            Config.Type = TheType;
            Config.Active = true;
            Config.StartDate = new DateTime(2020, 1, 1);
            Config.NumberOccurs = 1;
            Config.DateTime = new DateTime(2020, 1, 8, 14, 0, 0);
            DateTime CurrentDate = new DateTime(2020, 1, 4);
            var Result = TheGenerator.Calculate(CurrentDate, Config);
            Assert.NotNull(Result);
            Assert.NotNull(Result.NextExecution);
            Assert.Equal(NextDateExpected, Result.NextExecution.Value.ToString("dd/MM/yyyy HH:mm:ss"));
           

        }
    }
}
