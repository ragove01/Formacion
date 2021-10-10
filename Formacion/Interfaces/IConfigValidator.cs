using Formacion.Views;
using System;


namespace Formacion.Interfaces
{
    public interface IConfigValidator
    {
        void Validate(DateTime currentDate, SchedulerConfig config);
    }
}
