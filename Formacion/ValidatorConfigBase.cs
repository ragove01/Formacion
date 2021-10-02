using Formacion.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Formacion
{
    public class ValidatorConfigBase : IConfigValidator
    {
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
            if(config.IsValid() == false)
            {
                throw new ApplicationException("wrong configuration "); 
            }
            if(limits.IsValid() == false)
            {
                throw new ApplicationException("wrong limits");
            }
            if(limits.EndDate.HasValue && limits.EndDate < currentDate)
            {
                throw new ApplicationException("the end date cannot be earlier than the current date ");
            }
        }
    }
}
