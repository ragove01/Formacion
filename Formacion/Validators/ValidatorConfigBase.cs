using Formacion.Calculators;
using Formacion.TextsTranslations;
using Formacion.Views;
using System;

namespace Formacion.Validators
{
    public class ValidatorConfigBase 
    {
        private readonly ValidatorLimits validatorLimits;
        public ValidatorConfigBase()
        {
            this.validatorLimits = new ValidatorLimits(); 
        }
        public virtual void Validate(DateTime currentDate, SchedulerConfig config)
        {
            if (config is null)
            {
                throw new ApplicationException(Translator.GetText(TextsIndex.ConfigMustHasValue));
            }
            if(currentDate == DateTime.MaxValue)
            {
                throw new ApplicationException(Translator.GetText(TextsIndex.CurrentDateInvalid));
            }
            this.validatorLimits.Validate(config.StartDate, config.EndDate);
            
            
            if(CalculatorLastDateTimeCalc.GetLastDateTime(config) < currentDate)
            {
                throw new ApplicationException(Translator.GetText(TextsIndex.EndDateAerlierCurrentDate));
            }
        }

   
    }
}
