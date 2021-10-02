using System;
using System.Collections.Generic;
using System.Text;

namespace Formacion.Interfaces
{
    public interface IResult
    {
        DateTime NextExecution { get;  }
        DateTime StartDate { get;  }
    }
}
