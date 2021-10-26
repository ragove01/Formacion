using System;
using Formacion.Enums;
using Formacion.TextsTranslations;
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
                throw new ApplicationException(Translator.GetText(TextsIndex.WrongConfiguration));
            }
            if (config.DateTime.HasValue == false)
            {
                throw new ApplicationException(Translator.GetText(TextsIndex.DateTimeMustHasValue));
            }
      
        }
    }
}
