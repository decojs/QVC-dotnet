using Qvc.Executables;

namespace Qvc.Handlers
{
    public interface IHandleQuery<in TQuery, out TResult> : IHandleExecutable where TQuery : IQuery
    {
        TResult Handle(TQuery query);
    }
}