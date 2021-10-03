using System;
using System.Collections.Generic;
using System.Linq;
using Formacion.Enums;
using Formacion.Instantiators;
using Formacion.Interfaces;
using Formacion.Views;

namespace Formacion
{
    public class ScheluderGenerator
    {
       
        public SchedulerResults Calculate(DateTime currentDate, SchedulerConfig LosDatos)
        {
            IResultFormatter Formatter = Instantiators.InstantiatorFormatter.GetFormatter(LosDatos,LosDatos);
            ICalculator Calculator = Instantiators.InstantiatorCalculator.GetCalculator(LosDatos.Type);
            ICollection<IResult> TheResults = Calculator.Calulate(currentDate, LosDatos, LosDatos);
            SchedulerResults Result = new SchedulerResults(Formatter);
            Result.Results = TheResults.ToArray();
            return Result;
        }

     
    }
}
