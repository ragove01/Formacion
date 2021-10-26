using Formacion.Configs;
using Formacion.Enums;
using Formacion.TextsTranslations;
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
                throw new ApplicationException(Translator.GetText(TextsIndex.ConfigMustHasValue));
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
                throw new ApplicationException(Translator.GetText(TextsIndex.MustIndicateDayOfMonth));
            }
            if (configMontly.DayMonth < 1)
            {
                throw new ApplicationException(Translator.GetText(TextsIndex.MustIndicateDayGreatZero));
            }
            if (configMontly.DayMonth > 31)
            {
                throw new ApplicationException(Translator.GetText(TextsIndex.MustIndicateDayLes31));
            }
        }

        private void ValidateConfigEver(ConfigMonthly configMontly)
        {
            if (configMontly.TypesEvery.HasValue == false)
            {
                throw new ApplicationException(Translator.GetText(TextsIndex.MustIndicateTypeEveryDay));
            }
            if (configMontly.TypesDayEvery.HasValue == false)
            {
                throw new ApplicationException(Translator.GetText(TextsIndex.MustIndicateTypeOfDayWeek));
            }

        }
        private void ValidateNumberMonth(ConfigMonthly configMontly)
        {
            if (configMontly.EveryNumberMonths < 1 || configMontly.EveryNumberMonths > 12)
            {
                throw new ApplicationException(Translator.GetText(TextsIndex.MonthBeBetween1And12));
            }
            
        }
    }
}
