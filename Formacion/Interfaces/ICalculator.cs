using System;
using System.Collections.Generic;
using System.Text;

namespace Formacion.Interfaces
{
    public interface ICalculator
    {
        IConfigValidator Validator { get; }
        ICollection<IResult> Calulate(DateTime currentDate, IConfig config, ILimits limits);
    }
}
