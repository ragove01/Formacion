using System;
using Formacion.Enums;
using Formacion.Views;

namespace Formacion.Validators
{
    public class ValidatorConfigOnce:ValidatorConfigBase 
    {
        public override void Validate(DateTime currentDate, SchedulerConfig config)
        {
            base.Validate(currentDate,config);
            if (config.Type != TypesSchedule.Once)
            {
                throw new ApplicationException("wrong configuration ");
            }
            if (config.DateTime.HasValue == false)
            {
                throw new ApplicationException("Date Time must have a value ");
            }
      
        }
    }
}
