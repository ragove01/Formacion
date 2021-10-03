using System;


namespace Formacion.Interfaces
{
    public interface IConfigOnce:IConfig
    {
        DateTime? DateTime { get; set; }
    }
}
