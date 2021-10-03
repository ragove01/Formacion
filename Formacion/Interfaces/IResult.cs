using System;


namespace Formacion.Interfaces
{
    public interface IResult
    {
        DateTime NextExecution { get;  }
        DateTime StartDate { get;  }
    }
}
