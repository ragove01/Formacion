using Formacion.Enums;
using Formacion.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formacion.Instantiators
{
    public class InstantiatorValidator
    {
        public static ValidatorConfigBase GetValidator(TypesSchedule type)
        {
            if(type == TypesSchedule.Recurring)
            {
                return new ValidatorConfigRecurring(); 
            }
            return new ValidatorConfigOnce(); 
        }
    }
}
