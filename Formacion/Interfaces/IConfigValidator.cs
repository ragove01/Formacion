using System;
using System.Collections.Generic;
using System.Text;

namespace Formacion.Interfaces
{
    public interface IConfigValidator
    {
        void Validate(DateTime currentDate, IConfig config, ILimits limits);
    }
}
