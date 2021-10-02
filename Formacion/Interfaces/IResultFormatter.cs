using System;
using System.Collections.Generic;
using System.Text;

namespace Formacion.Interfaces
{
    public interface IResultFormatter
    {
        IConfig Config { get; }
        ILimits Limits { get; }
        string Formatter(IResult result);

    }
}
