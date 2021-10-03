using Formacion.Interfaces;
using System;

namespace Formacion.Validators
{
    public class ValidatorLimits : ILimitsValidator
    {
        public void Validate(ILimits limits)
        {
            if(limits.EndDate.HasValue &&
                limits.EndDate < limits.StartDate)
            {
                throw new ApplicationException("End date must be great than start date ");
            }
        }
    }
}
