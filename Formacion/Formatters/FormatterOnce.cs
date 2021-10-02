using Formacion.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Formacion.Formatters
{
    public class FormatterOnce : IResultFormatter
    {
        private readonly IConfigOnce config;
        private readonly ILimits limits;
        public FormatterOnce(IConfigOnce TheConfig, ILimits TheLimits)
        {
            this.config = TheConfig;
            this.limits = TheLimits;
        }
        public IConfig Config => this.config;

        public ILimits Limits => this.limits;

        public string Formatter(IResult result)
        {
            return $"Occurs once. Schedule will be used on {result.NextExecution.ToString("dd/MM/yyyy")} at {result.NextExecution.ToString("HH:mm")} " +
                $" starting on {result.StartDate.ToString("dd/MM/yyyy")}";
        }
    }
}
