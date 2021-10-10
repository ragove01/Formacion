using System;
using Formacion.Enums;
using Formacion.Interfaces;

namespace Formacion.Validators
{
    public class ValidatorConfigOnce:ValidatorConfigBase 
    {
        public override void Validate(DateTime currentDate, IConfig config, ILimits limits)
        {
            base.Validate(currentDate,config, limits);
            if (config.Type != TypesSchedule.Once)
            {
                throw new ApplicationException("wrong configuration ");
            }
            if (((IConfigOnce)config).DateTime.HasValue == false)
            {
                throw new ApplicationException("Date Time must have a value ");
            }
      
        }
    }
}
