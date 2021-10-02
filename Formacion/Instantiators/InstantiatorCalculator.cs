using Formacion.Interfaces;
using Formacion.Calculators;


namespace Formacion.Instantiators
{
    public class InstantiatorCalculator
    {
        public static ICalculator GetCalculator(TypesSchedule Type)
        {
            if(Type == TypesSchedule.Recurring)
            {
                return new CalculatorRecurring();
            }
            return new CalculatorOnce();
        }
    }
}
