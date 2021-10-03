using Formacion;
using Formacion.Enums;
using Formacion.Instantiators;
using Formacion.Validators;
using Formacion.Views;
using System;
using Xunit;

namespace Testing
{
    public class TestValidator
    {
        [Fact]
        public void Test_Validator_Config_Once()
        {
            SchedulerConfig schedulerData = new SchedulerConfig();
            DateTime CurrentDate = new DateTime(2020, 1, 4);
           
            schedulerData.StartDate = new DateTime(2020, 1, 1);

            ValidatorConfigOnce validator = new ValidatorConfigOnce();
            schedulerData.Type = TypesSchedule.Recurring;
            
            var TheException = Assert.Throws<ApplicationException>(() => validator.Validate(CurrentDate, schedulerData, schedulerData));
            Assert.Equal("wrong configuration ", TheException.Message);
            schedulerData.Type = TypesSchedule.Once;
            TheException = Assert.Throws<ApplicationException>(() => validator.Validate(CurrentDate, schedulerData, schedulerData));
            Assert.Equal("Date Time must have a value ", TheException.Message);
            schedulerData.EndDate = new DateTime(2019, 12, 31);
            TheException = Assert.Throws<ApplicationException>(() => validator.Validate(CurrentDate, schedulerData, schedulerData));
            Assert.Equal("End date must be great than start date ", TheException.Message);
            schedulerData.EndDate = null;
            schedulerData.DateTime = new DateTime(2020, 1, 4, 14, 0, 0);
            validator.Validate(CurrentDate, schedulerData, schedulerData);

        }
        
        [Fact]
        public void Test_Validator_Config_Recurring()
        {
            SchedulerConfig schedulerData = new SchedulerConfig();
            DateTime CurrentDate = new DateTime(2020, 1, 4);
            
           
            ValidatorConfigRecurring validator = new ValidatorConfigRecurring();
            var TheException =  Assert.Throws<ApplicationException>(() => validator.Validate(CurrentDate, schedulerData, schedulerData));
            Assert.Equal("wrong configuration ", TheException.Message);
            schedulerData.Type = TypesSchedule.Recurring;
            TheException = Assert.Throws<ApplicationException>(() => validator.Validate(CurrentDate, schedulerData, schedulerData));
            Assert.Equal("Número of occurs must be great than zero ", TheException.Message);
            schedulerData.StartDate = new DateTime(2020, 1, 1);
            TheException =  Assert.Throws<ApplicationException>(() => validator.Validate(CurrentDate, schedulerData, schedulerData));
            Assert.Equal("Número of occurs must be great than zero ", TheException.Message);
            schedulerData.EndDate = new DateTime(2019, 12, 31);
            TheException = Assert.Throws<ApplicationException>(() => validator.Validate(CurrentDate, schedulerData, schedulerData));
            Assert.Equal("End date must be great than start date ", TheException.Message);
            schedulerData.EndDate = null;
            schedulerData.NumberOccurs = 1;
            validator.Validate(CurrentDate, schedulerData, schedulerData);
        }
        
    }
}
