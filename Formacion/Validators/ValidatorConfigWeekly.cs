using Formacion.Configs;
using Formacion.TextsTranslations;
using System;

namespace Formacion.Validators
{
    public class ValidatorConfigWeekly
    {
        public void Validate(ConfigWeekly configWeekly)
        {
            if(configWeekly == null)
            {
                throw new ApplicationException(Translator.GetText(TextsIndex.ConfigMustHasValue));
            }
            if(configWeekly.Every < 1)
            {
                throw new ApplicationException(Translator.GetText(TextsIndex.EveryMustGreatZero));
            }

            if(!(configWeekly.Monday || 
               configWeekly.Tuesday ||
               configWeekly.Wednesday ||
               configWeekly.Thursday ||
               configWeekly.Friday ||
                configWeekly.Saturday ||
                configWeekly.Sunday))
            {
                throw new ApplicationException(Translator.GetText(TextsIndex.MustSelectDayWeek));
            }
        }
    }
}
