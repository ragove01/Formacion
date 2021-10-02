using Formacion.Formatters;
using Formacion.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Formacion.Instantiators
{
    public class InstantiatorFormatter
    {
        
        public static IResultFormatter GetFormatter(IConfig config,ILimits limits)
        {
            if(config is IConfigOnce)
            {
                return GetFormaterOnce((IConfigOnce)config, limits);
            }
            if(config is IConfigRecurring)
            {
                return GetFormaterRecurring((IConfigRecurring)config, limits);
            }
            throw new ApplicationException("Formatter not implemented");
        }
        private static IResultFormatter GetFormaterOnce(IConfigOnce config, ILimits limits)
        {
            
                return new FormatterOnce(config, limits);
           
        }
        private static IResultFormatter GetFormaterRecurring(IConfigRecurring config, ILimits limits)
        {

            return new FormatterRecurring(config, limits);

        }
    }
}
