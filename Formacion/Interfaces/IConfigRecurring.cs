
using Formacion.Enums;

namespace Formacion.Interfaces
{
    public interface IConfigRecurring:IConfig
    {
        TypesOccurs Occurs { get; set; }
        int NumberOccurs { get; set; }
    }
}
