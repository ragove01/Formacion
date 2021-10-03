using Formacion.Interfaces;
using Formacion.Validators;
using System;
using System.Collections.Generic;


namespace Formacion.Calculators
{
    public class CalculatorOnce : CalculatorBase 
    {
     
        public CalculatorOnce():base(new ValidatorConfigOnce())
        {
           
        }
        public override ICollection<IResult> Calulate(DateTime currentDate,IConfig config, ILimits limits)
        {
            this.Validate(currentDate, config, limits);
            if(config.Active == false)
            {
                return null;
            }
            return new IResult[]{ new Result(limits.StartDate,((IConfigOnce)config).DateTime.Value) };
        }
    }
}
