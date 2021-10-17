using Formacion.Calculators;
using Formacion.Configs;
using Formacion.Enums;
using Formacion.Extensions;
using Formacion.Views;
using System;

namespace Formacion.Validators
{
    public class ValidatorConfigBase 
    {
        private readonly ValidatorLimits validatorLimits;
        public ValidatorConfigBase()
        {
            this.validatorLimits = new ValidatorLimits(); 
        }
        public virtual void Validate(DateTime currentDate, SchedulerConfig config)
        {
            if (config is null)
            {
                throw new ApplicationException("Config must have a value ");
            }
            if(currentDate == DateTime.MaxValue)
            {
                throw new ApplicationException("The current date is invalid");
            }
            this.validatorLimits.Validate(config.StartDate, config.EndDate);
            
            
            if(CalculatorLastDateTimeCalc.GetLastDateTime(config) < currentDate)
            {
                throw new ApplicationException("the end date cannot be earlier than the current date ");
            }
        }

   
    }
}
