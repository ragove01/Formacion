using Formacion.Calculators;
using Formacion.Enums;

namespace Formacion.Instantiators
{
    public class InstantiatorCalculator
    {
        public static CalculatorBase GetCalculator(TypesSchedule type)
        {
            if(type == TypesSchedule.Recurring)
            {
                return new CalculatorRecurring();
            }
            return new CalculatorOnce();
        }
    }
}
