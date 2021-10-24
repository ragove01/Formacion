using Formacion.Configs;
using Formacion.Enums;
using System;

namespace Formacion.Validators
{
    public class ValidatorConfigDailyFrecuency
    {
        public void Validate(ConfigDailyFrecuency configDailyFrecuenci)
        {
            if(configDailyFrecuenci == null)
            {
                throw new ApplicationException(Texts.ConfigMustHasValue);
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
                throw new ApplicationException(Texts.OnceAtValue); 
            }
        }

        private void ValidateEvery(ConfigDailyFrecuency configDailyFrecuenci)
        {
            if (configDailyFrecuenci.NumberOccurs < 0)
            {
                throw new ApplicationException(Texts.OccursGreatZero);
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
                throw new ApplicationException(Texts.StartingAtNotHasValue);
            }
            if (configDailyFrecuenci.EndTime.HasValue == false)
            {
                throw new ApplicationException(Texts.EndAtNotHasValue);
            }
            if(configDailyFrecuenci.StartTime.Value  > configDailyFrecuenci.EndTime.Value)
            {
                throw new ApplicationException(Texts.EndAtMinorStartingAt);
            }
        }

        

    }
}
