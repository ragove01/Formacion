using Formacion.Enums;
using Formacion.Interfaces;
using System;

namespace Formacion.Configs
{
    public class ConfigOnce : IConfigOnce
    {
        public DateTime? DateTime { get; set; }
        public TypesSchedule Type { get; set; }
        public bool Active { get; set; }

        public bool IsValid()
        {
            if(this.Type != TypesSchedule.Once)
            {
                return false;
            }
            if(this.DateTime.HasValue == false)
            {
                return false;
            }
            return true;
        }
    }
}
