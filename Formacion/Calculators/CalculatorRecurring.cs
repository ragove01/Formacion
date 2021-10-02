using Formacion.Interfaces;
using Formacion.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Formacion.Calculators
{
    public class CalculatorRecurring : CalculatorBase
    {
        private ICalculatorNextExecutionTime calculatorNexExecution;
        
        public CalculatorRecurring() : base(new ValidatorConfigRecurring())
        {

        }
        public override ICollection<IResult> Calulate(DateTime currentDate, IConfig config, ILimits limits)
        {
            this.calculatorNexExecution = new CalculatorNextExecutionTime(((IConfigRecurring)config).Occurs); 
            this.Validate(currentDate, config, limits);
            if (config.Active == false)
            {
                return null; 
            }
            List<IResult> ReturnValue = new List<IResult>();
            DateTime NextDate = currentDate < limits.StartDate ? limits.StartDate:currentDate;
            NextDate = this.calculatorNexExecution.GetNext(NextDate);
            for (int Index = 0; Index < ((IConfigRecurring)config).NumberOccurs
                && this.Continue(NextDate, limits.EndDate); Index++)
            {
                ReturnValue.Add(new Result(limits.StartDate, NextDate));
                NextDate = this.calculatorNexExecution.GetNext(NextDate);
            }
            return ReturnValue;
        }
        
        private bool Continue(DateTime CurrentDate, DateTime? EndDate)
        {
            if(EndDate.HasValue == false)
            {
                return true;
            }
            return CurrentDate < EndDate.Value;
        }
    }
}
