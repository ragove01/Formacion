using Formacion.Validators;
using Formacion.Views;
using System;

namespace Formacion.Calculators
{
    public class CalculatorRecurring : CalculatorBase
    {
        
        public CalculatorRecurring() : base(new ValidatorConfigRecurring())
        {
            
        }
        public override DateTime Calculate(DateTime currentDate, SchedulerConfig config)
        {

            this.ValidatePrivate(currentDate,config);
            if (config.Active == false)
            {
                return currentDate;
            }
            return this.CalculatePrivate(currentDate, config);
 
        }

        private void ValidatePrivate(DateTime currentDate, SchedulerConfig config)
        {
            
            this.Validate(currentDate, config);
        }

        private DateTime CalculatePrivate(DateTime currentDate, SchedulerConfig config)
        {

            return new CalculatorNextExecutionTime(config).GetNext(currentDate);
        }
   
    }
}
