using Formacion.Configs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Formacion.Validators
{
    public class ValidatorConfigWeekly
    {
        public void Validate(ConfigWeekly configWeekly)
        {
            if(configWeekly == null)
            {
                throw new ApplicationException("Config must have a value ");
            }
            if(configWeekly.Every < 1)
            {
                throw new ApplicationException("Weekly configuration: 'Every' muste be greater than zero");
            }

            if(!(configWeekly.Monday || 
               configWeekly.Tuesday ||
               configWeekly.Wednesday ||
               configWeekly.Thursday ||
               configWeekly.Friday ||
                configWeekly.Saturday ||
                configWeekly.Sunday))
            {
                throw new ApplicationException("Weekly configuration: must select one or more days of the week");
            }
        }
    }
}
