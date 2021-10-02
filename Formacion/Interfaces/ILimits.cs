using System;
using System.Collections.Generic;
using System.Text;

namespace Formacion.Interfaces
{
    public interface ILimits
    {
        DateTime StartDate { get; set; }
        DateTime? EndDate { get; set; }

        bool IsValid();
    }
}
