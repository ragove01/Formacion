using System;
using Formacion.Enums;
using Formacion.Interfaces;

namespace Formacion.Validators
{
    public class ValidatorConfigRecurring:ValidatorConfigBase
    {
        
        public override void Validate(DateTime currentDate, IConfig config, ILimits limits)
        {
            base.Validate(currentDate,config, limits);
            if (config.Type != TypesSchedule.Recurring)
            {
                throw new ApplicationException("wrong configuration ");
            }
            if (((IConfigRecurring)config).NumberOccurs <= 0)
            {
                throw new ApplicationException("Número of occurs must be great than zero ");
            }
        }
    }
}
