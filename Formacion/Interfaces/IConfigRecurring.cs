using System;
using System.Collections.Generic;
using System.Text;

namespace Formacion.Interfaces
{
    public interface IConfigRecurring:IConfig
    {
        TypesOccurs Occurs { get; set; }
        int NumberOccurs { get; set; }
    }
}
