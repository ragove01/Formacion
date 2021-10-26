using Formacion.TextsTranslations;
using System;

namespace Formacion.Validators
{
    public class ValidatorLimits 
    {
        public void Validate(DateTime startDate, DateTime? endDate)
        {
            if(startDate == DateTime.MaxValue)
            {
                throw new ApplicationException(Translator.GetText(TextsIndex.StartDateInvalid));
            }
            if(endDate.HasValue &&
                endDate < startDate)
            {
                throw new ApplicationException(Translator.GetText(TextsIndex.EndDateGreatStartDate));
            }
        }
    }
}
