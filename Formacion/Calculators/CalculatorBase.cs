using Formacion.Validators;
using Formacion.Views;
using System;

namespace Formacion.Calculators
{
    public abstract class CalculatorBase 
    {
        private readonly ValidatorConfigBase validator;
        public ValidatorConfigBase Validator => validator;
        public CalculatorBase(ValidatorConfigBase theValidator)
        {
            this.validator = theValidator;
        }
        public abstract DateTime Calculate(DateTime currentDate, SchedulerConfig config);
        
        protected virtual void Validate(DateTime currentDate, SchedulerConfig config)
        {
            if(this.Validator == null)
            {
                throw new ApplicationException("Validator must have a value");
            }
            this.Validator.Validate(currentDate, config);
        }
    }
}
