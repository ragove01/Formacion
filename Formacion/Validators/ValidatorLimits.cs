using Formacion.Interfaces;
using System;

namespace Formacion.Validators
{
    public class ValidatorLimits 
    {
        public void Validate(DateTime startDate, DateTime? endDate)
        {
            if(startDate == DateTime.MaxValue)
            {
                throw new ApplicationException("Start Date is invalid");
            }
            if(endDate.HasValue &&
                endDate < startDate)
            {
                throw new ApplicationException("End date must be great than start date ");
            }
        }
    }
}
