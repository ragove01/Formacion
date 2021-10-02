using Formacion.Configs;
using Formacion.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Formacion.Instantiators
{
    public class InstantiatorConfiguratior
    {
        public static IConfig GetConfiguracion(TypesSchedule Type)
        {
            
            if(Type == TypesSchedule.Recurring)
            {
                return new ConfigRecurring();
            }
            return new ConfigOnce();
        }
    }
}
