using System;
using Formacion.Enums;
using Formacion.Views;

namespace Formacion.Validators
{
    public class ValidatorConfigRecurring:ValidatorConfigBase
    {

        public override void Validate(DateTime currentDate, SchedulerConfig config)
        {
            this.ValidateConfig(currentDate, config);

            this.ValidateSchedulerConfig(config);

        }
        private void ValidateConfig(DateTime currentDate, SchedulerConfig config)
        {
            base.Validate(currentDate, config);
            if (config.Type != TypesSchedule.Recurring)
            {
                throw new ApplicationException("wrong configuration ");
            }
            if (config.DailyFrecuenci == null && config.Weekly == null && config.NumberOccurs <= 0)
            {
                throw new ApplicationException("Número of occurs must be great than zero ");
            }
        }
        private void ValidateSchedulerConfig(SchedulerConfig config)
        {
            if(config.DailyFrecuenci !=null)
            {
                new ValidatorConfigDailyFrecuency().Validate(config.DailyFrecuenci); 
            }
            if(config.Weekly != null)
            {
                new ValidatorConfigWeekly().Validate(config.Weekly); 
            }
            if(config.Monthly != null)
            {
                new ValidatorConfigMonthly().Validate(config.Monthly);
            }
        }
    }
}
