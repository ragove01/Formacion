using Formacion.Configs;
using Formacion.Enums;
using Formacion.TextsTranslations;
using System;

namespace Formacion.Validators
{
    public class ValidatorConfigDailyFrecuency
    {
        public void Validate(ConfigDailyFrecuency configDailyFrecuenci)
        {
            if(configDailyFrecuenci == null)
            {
                throw new ApplicationException(Translator.GetText(TextsIndex.ConfigMustHasValue));
            }
            if(configDailyFrecuenci.Frecuenci == TypesOccursDailyFrecuency.Once)
            {
                this.ValidateOnce(configDailyFrecuenci);
            }
            else
            {
                this.ValidateEvery(configDailyFrecuenci);
            }
        }

        private void ValidateOnce(ConfigDailyFrecuency configDailyFrecuenci)
        {
            if(configDailyFrecuenci.OnceTime.HasValue == false)
            {
                throw new ApplicationException(Translator.GetText(TextsIndex.OnceAtValue)); 
            }
        }

        private void ValidateEvery(ConfigDailyFrecuency configDailyFrecuenci)
        {
            if (configDailyFrecuenci.NumberOccurs < 0)
            {
                throw new ApplicationException(Translator.GetText(TextsIndex.OccursGreatZero));
            }
            if (configDailyFrecuenci.NumberOccurs > 0)
            {
                this.ValidateStartFinTime(configDailyFrecuenci); 
            }
        }

        private void ValidateStartFinTime(ConfigDailyFrecuency configDailyFrecuenci)
        {
           
            if(configDailyFrecuenci.StartTime.HasValue == false)
            {
                throw new ApplicationException(Translator.GetText(TextsIndex.StartingAtNotHasValue));
            }
            if (configDailyFrecuenci.EndTime.HasValue == false)
            {
                throw new ApplicationException(Translator.GetText(TextsIndex.EndAtNotHasValue));
            }
            if(configDailyFrecuenci.StartTime.Value  > configDailyFrecuenci.EndTime.Value)
            {
                throw new ApplicationException(Translator.GetText(TextsIndex.EndAtMinorStartingAt));
            }
        }

        

    }
}
