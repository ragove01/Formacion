using Formacion.Configs;
using Formacion.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Formacion.Validators
{
    public class ValidatorConfigMonthly
    {
        public void Validate(ConfigMonthly configMontly)
        {
            if (configMontly == null)
            {
                throw new ApplicationException(Texts.ConfigMustHasValue);
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

        private void ValidateConfigDay(ConfigMonthly configMontly)
        {
            if (configMontly.DayMonth.HasValue == false)
            {
                throw new ApplicationException(Texts.MustIndicateDayOfMonth);
            }
            if (configMontly.DayMonth < 1)
            {
                throw new ApplicationException(Texts.MustIndicateDayGreatZero);
            }
            if (configMontly.DayMonth > 31)
            {
                throw new ApplicationException(Texts.MustIndicateDayLes31);
            }
        }

        private void ValidateConfigEver(ConfigMonthly configMontly)
        {
            if (configMontly.TypesEvery.HasValue == false)
            {
                throw new ApplicationException(Texts.MustIndicateTypeEveryDay);
            }
            if (configMontly.TypesDayEvery.HasValue == false)
            {
                throw new ApplicationException(Texts.MustIndicateTypeOfDayWeek);
            }

        }
        private void ValidateNumberMonth(ConfigMonthly configMontly)
        {
            if (configMontly.EveryNumberMonths < 1 || configMontly.EveryNumberMonths > 12)
            {
                throw new ApplicationException(Texts.MonthBeBetween1And12);
            }
            
        }
    }
}
