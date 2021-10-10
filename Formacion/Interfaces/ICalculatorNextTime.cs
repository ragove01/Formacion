using System;
using Formacion.Enums;

namespace Formacion.Interfaces
{
    public interface ICalculatorNextExecutionTime
    {
        TypesOccurs Type { get; }
        DateTime GetNext(DateTime NextTime);
    }
}
