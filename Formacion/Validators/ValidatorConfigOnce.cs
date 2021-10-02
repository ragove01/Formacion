using System;
using System.Collections.Generic;
using System.Text;
using Formacion.Interfaces;

namespace Formacion.Validators
{
    public class ValidatorConfigOnce:ValidatorConfigBase 
    {
        public override void Validate(DateTime currentDate, IConfig config, ILimits limits)
        {
            base.Validate(currentDate,config, limits);
        }
    }
}
