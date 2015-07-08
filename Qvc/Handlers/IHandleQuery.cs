using System.Threading.Tasks;

using Qvc.Executables;

namespace Qvc.Handlers
{
    public interface IHandleQuery<in TQuery, out TResult> : IHandleExecutable
        where TQuery : IQuery
    {
        TResult Handle(TQuery query);
    }

    public interface IHandleQueryAsync<in TQuery, TResult> : IHandleExecutable
        where TQuery : IQuery
    {
        Task<TResult> Handle(TQuery query);
    }
}