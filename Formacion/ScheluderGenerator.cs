using System;
using System.Collections.Generic;
using System.Linq;
using Formacion.Instantiators;
using Formacion.Interfaces;

namespace Formacion
{
    public class ScheluderGenerator
    {
        private ICalculator calculator;
        private ICollection<IResult> results;
        private IResultFormatter formatter;

        public ScheluderGenerator()
        {
            this.results = null;
            this.formatter = null;
        }

        public IConfig GetConfig(TypesSchedule Type)
        {
            return InstantiatorConfiguratior.GetConfiguracion(Type);
        }

        public void Calculate(DateTime currentDate, IConfig config, ILimits limits)
        {
            this.formatter = Instantiators.InstantiatorFormatter.GetFormatter(config, limits);
            ICalculator Calculator = Instantiators.InstantiatorCalculator.GetCalculator(config.Type);
            this.results = Calculator.Calulate(currentDate, config, limits);

        }

        public DateTime? NextExecution
        {
            get
            {
                if(this.results == null)
                {
                    throw new ApplicationException("Scheluder not calculate"); 
                }
                return this.results.FirstOrDefault()?.NextExecution;
            }
        }
        public IResult[] Results
        {
            get
            {
                if (this.results == null)
                {
                    throw new ApplicationException("Scheluder not calculate");
                }
                return this.results.ToArray(); 
            }
        }
        
        public string NextExecutionTimeString
        {
            get
            {
                if (this.results == null)
                {
                    throw new ApplicationException("Scheluder not calculate");
                }
                return this.formatter.Formatter(this.results.FirstOrDefault());
            }
        }



    }
}
