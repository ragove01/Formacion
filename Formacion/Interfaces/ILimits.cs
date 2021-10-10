using System;


namespace Formacion.Interfaces
{
    public interface ILimits
    {
        DateTime StartDate { get; set; }
        DateTime? EndDate { get; set; }

    }
}
