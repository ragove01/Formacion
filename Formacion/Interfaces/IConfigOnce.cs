using System;
using System.Collections.Generic;
using System.Text;

namespace Formacion.Interfaces
{
    public interface IConfigOnce:IConfig
    {
        DateTime? DateTime { get; set; }
    }
}
