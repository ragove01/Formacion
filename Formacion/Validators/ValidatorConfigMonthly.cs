using Formacion.Configs;
using Formacion.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Formacion.Validators
{
    public class ValidatorConfigMonthly
    {
        public void Validate(ConfigMontly configMontly)
        {
            if (configMontly == null)
            {
                throw new ApplicationException("Config must have a value ");
            }

            if (configMontly.Type == TypesMontlyFrecuency.Day)
            {
                this.ValidateConfigDay(configMontly);
            }
            else
            {
                this.ValidateConfigEver(configMontly);
            }
            this.ValidateNumberMonth(configMontly);
        }

        private void ValidateConfigDay(ConfigMontly configMontly)
        {
            if (configMontly.DayMonth.HasValue == false)
            {
                throw new ApplicationException("Montly configuration: must indicate a day of the month ");
            }
            if (configMontly.DayMonth < 1)
            {
                throw new ApplicationException("Montly configuration: must indicate a day of the month great than zero ");
            }
            if (configMontly.DayMonth > 31)
            {
                throw new ApplicationException("Montly configuration: must indicate a day of the month less or equal than 31 ");
            }
        }

        private void ValidateConfigEver(ConfigMontly configMontly)
        {
            if (configMontly.TypesEvery.HasValue == false)
            {
                throw new ApplicationException("Montly configuration: must indicate the type every day");
            }
            if (configMontly.TypesDayEvery.HasValue == false)
            {
                throw new ApplicationException("Montly configuration: must indicate a type of day week ");
            }

        }
        private void ValidateNumberMonth(ConfigMontly configMontly)
        {
            if (configMontly.EveryNumberMonths < 1 || configMontly.EveryNumberMonths > 12)
            {
                throw new ApplicationException("Montly configuration: month(s) must be between 1 and 12 ");
            }
            
        }
    }
}
