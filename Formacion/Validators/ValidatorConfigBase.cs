using Formacion.Interfaces;
using System;

namespace Formacion.Validators
{
    public class ValidatorConfigBase : IConfigValidator
    {
        private readonly ILimitsValidator validatorLimits;
        public ValidatorConfigBase()
        {
            this.validatorLimits = new ValidatorLimits(); 
        }
        public virtual void Validate(DateTime currentDate, IConfig config, ILimits limits)
        {
            if (config is null)
            {
                throw new ApplicationException("Config must have a value ");
            }
            if (limits is null)
            {
                throw new ApplicationException("Limits must have a value ");
            }
            this.validatorLimits.Validate(limits);
            
            
            if(limits.EndDate.HasValue && limits.EndDate < currentDate)
            {
                throw new ApplicationException("the end date cannot be earlier than the current date ");
            }
        }
    }
}
