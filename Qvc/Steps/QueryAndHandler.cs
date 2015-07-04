using Qvc.Executables;
using Qvc.Handlers;

namespace Qvc.Steps
{
    public class QueryAndHandler
    {
        public IQuery Query { get; private set; }

        public IHandleExecutable Handler { get; private set; }

        public QueryAndHandler(IQuery query, IHandleExecutable handler)
        {
            Query = query;
            Handler = handler;
        }
    }
}