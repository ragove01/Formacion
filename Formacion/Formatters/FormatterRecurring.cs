using Formacion.Interfaces;

namespace Formacion.Formatters
{
    public class FormatterRecurring:IResultFormatter
    {
        private readonly IConfigRecurring config;
        private readonly ILimits limits;
        public FormatterRecurring(IConfigRecurring TheConfig, ILimits TheLimits)
        {
            this.config = TheConfig;
            this.limits = TheLimits;
        }
        public IConfig Config => this.config;

        public ILimits Limits => this.limits;

        public string Formatter(IResult result)
        {
            return $"Occurs every {this.config.Occurs.ToString().ToLower()}. Schedule will be used on {result.NextExecution.ToString("dd/MM/yyyy")} at {result.NextExecution.ToString("HH:mm")}" +
                $" starting on {result.StartDate.ToString("dd/MM/yyyy")}";
        }

        
    }
}
