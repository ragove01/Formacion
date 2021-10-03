

namespace Formacion.Interfaces
{
    public interface IResultFormatter
    {
        IConfig Config { get; }
        ILimits Limits { get; }
        string Formatter(IResult result);

    }
}
