using Formacion.Validators;
using Formacion.Views;
using System;

namespace Formacion.Calculators
{
    public class CalculatorBase 
    {
        private readonly ValidatorConfigBase validator;
        public ValidatorConfigBase Validator => validator;
        public CalculatorBase(ValidatorConfigBase theValidator)
        {
            this.validator = theValidator;
        }
        public virtual DateTime Calculate(DateTime currentDate, SchedulerConfig config)
        {
            throw new NotImplementedException();
        }
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
