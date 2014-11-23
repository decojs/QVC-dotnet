using Qvc.Executables;

namespace Qvc.Handlers
{
    public interface IHandleQuery<in TQuery, out TResult> where TQuery : IQuery
    {
        TResult Handle(TQuery query);
    }
}