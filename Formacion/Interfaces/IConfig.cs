
using Formacion.Enums;
namespace Formacion.Interfaces
{
    public interface IConfig
    {
        TypesSchedule Type { get; set; }
        bool Active { get; set; }
       
    }
}
