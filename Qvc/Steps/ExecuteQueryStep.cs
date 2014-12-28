using System;
using Qvc.Executables;
using Qvc.Handlers;
using Qvc.Results;

namespace Qvc.Steps
{
    public class ExecuteQueryStep
    {
        private readonly IQuery _query;
        private readonly IHandleExecutable _handler;

        public ExecuteQueryStep(IQuery query, IHandleExecutable handler)
        {
            _query = query;
            _handler = handler;
        }
        
        public SerializeResultStep HandleQuery(Func<IHandleExecutable, IQuery, object> executeQuery)
        {
            var result = executeQuery.Invoke(_handler, _query);
            return new SerializeResultStep(new QueryResult(result));
        }
    }
}