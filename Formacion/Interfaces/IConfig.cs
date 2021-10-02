using System;
using System.Collections.Generic;
using System.Text;

namespace Formacion.Interfaces
{
    public interface IConfig
    {
        TypesSchedule Type { get; set; }
        bool Active { get; set; }
        bool IsValid();
    }
}
