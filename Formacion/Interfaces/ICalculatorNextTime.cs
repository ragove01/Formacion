using System;
using System.Collections.Generic;
using System.Text;

namespace Formacion.Interfaces
{
    public interface ICalculatorNextExecutionTime
    {
        TypesOccurs Type { get; }
        DateTime GetNext(DateTime NextTime);
    }
}
