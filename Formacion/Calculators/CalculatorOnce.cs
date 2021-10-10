using Formacion.Interfaces;
using Formacion.Validators;
using Formacion.Views;
using System;
using System.Collections.Generic;


namespace Formacion.Calculators
{
    public class CalculatorOnce : CalculatorBase 
    {
     
        public CalculatorOnce():base(new ValidatorConfigOnce())
        {
           
        }
        public override DateTime Calculate(DateTime currentDate,SchedulerConfig config)
        {
            this.Validate(currentDate, config);
            
            return config.DateTime.Value;
        }
    }
}
