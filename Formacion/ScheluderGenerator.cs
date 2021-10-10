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
       
        public SchedulerResults Calculate(DateTime currentDate, SchedulerConfig scheduleConfig)
        {
            IResultFormatter Formatter = Instantiators.InstantiatorFormatter.GetFormatter(scheduleConfig,scheduleConfig);
            ICalculator Calculator = Instantiators.InstantiatorCalculator.GetCalculator(scheduleConfig.Type);
            ICollection<IResult> TheResults = Calculator.Calulate(currentDate, scheduleConfig, scheduleConfig);
            SchedulerResults Result = new SchedulerResults(Formatter);
            Result.Results = TheResults.ToArray();
            return Result;
        }

     
    }
}
