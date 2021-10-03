using Formacion.Enums;
using Formacion.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Formacion.Configs
{
    public class ConfigRecurring : IConfigRecurring
    {
        public TypesOccurs Occurs { get; set; }
        public int NumberOccurs { get; set; }
        public TypesSchedule Type { get; set; }
        public bool Active { get; set; }

        public bool IsValid()
        {
            if(Type != TypesSchedule.Recurring)
            {
                return false;
            }
            if(NumberOccurs <= 0)
            {
                return false;
            }
            return true;
        }
    }
}
