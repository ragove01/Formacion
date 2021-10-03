using System;


namespace Formacion.Interfaces
{
    public interface IConfigValidator
    {
        void Validate(DateTime currentDate, IConfig config, ILimits limits);
    }
}
