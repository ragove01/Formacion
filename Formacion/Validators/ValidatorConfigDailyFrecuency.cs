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
                throw new ApplicationException("Config must have a value ");
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
                throw new ApplicationException("'Once at' must have a value"); 
            }
        }

        private void ValidateEvery(ConfigDailyFrecuency configDailyFrecuenci)
        {
            if (configDailyFrecuenci.NumberOccurs < 0)
            {
                throw new ApplicationException("'Occurs every' must be greater or equal than zero");
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
                throw new ApplicationException("'Starting at' must have a value");
            }
            if (configDailyFrecuenci.EndTime.HasValue == false)
            {
                throw new ApplicationException("'End at' must have a value");
            }
            if(configDailyFrecuenci.StartTime.Value  > configDailyFrecuenci.EndTime.Value)
            {
                throw new ApplicationException("'End at' must be great than 'Starting at'");
            }
        }

        

    }
}
