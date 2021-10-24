using Formacion.Configs;
using System;

namespace Formacion.Validators
{
    public class ValidatorConfigWeekly
    {
        public void Validate(ConfigWeekly configWeekly)
        {
            if(configWeekly == null)
            {
                throw new ApplicationException(Texts.ConfigMustHasValue);
            }
            if(configWeekly.Every < 1)
            {
                throw new ApplicationException(Texts.EveryMustGreatZero);
            }

            if(!(configWeekly.Monday || 
               configWeekly.Tuesday ||
               configWeekly.Wednesday ||
               configWeekly.Thursday ||
               configWeekly.Friday ||
                configWeekly.Saturday ||
                configWeekly.Sunday))
            {
                throw new ApplicationException(Texts.MustSelectDayWeek);
            }
        }
    }
}
