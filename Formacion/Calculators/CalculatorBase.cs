using Formacion.Interfaces;
using Formacion.Views;
using System;
using System.Collections.Generic;

namespace Formacion.Calculators
{
    public class CalculatorBase 
    {
        private readonly IConfigValidator validator;
        public IConfigValidator Validator => validator;
        public CalculatorBase(IConfigValidator theValidator)
        {
            this.validator = theValidator;
        }
        public virtual DateTime Calculate(DateTime currentDate, SchedulerConfig config)
        {
            throw new NotFiniteNumberException();
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
