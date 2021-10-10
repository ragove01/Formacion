using Formacion.Interfaces;
using System;
using System.Collections.Generic;

namespace Formacion.Calculators
{
    public class CalculatorBase : ICalculator
    {
        private readonly IConfigValidator validator;
        public IConfigValidator Validator => validator;
        public CalculatorBase(IConfigValidator theValidator)
        {
            this.validator = theValidator;
        }
        public virtual ICollection<IResult> Calulate(DateTime currentDate, IConfig config, ILimits limits)
        {
            throw new NotFiniteNumberException();
        }
        protected virtual void Validate(DateTime currentDate, IConfig config,ILimits limits)
        {
            if(this.Validator == null)
            {
                throw new ApplicationException("Validator must have a value");
            }
            this.Validator.Validate(currentDate, config, limits);
        }
    }
}
