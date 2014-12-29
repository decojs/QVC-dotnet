using System;
using Qvc.Executables;
using Qvc.Handlers;
using Qvc.Results;

namespace Qvc.Steps.Implementations
{
    public class DontExecuteQueryStep : IExecuteQueryStep
    {
        private readonly Exception _exception;

        public DontExecuteQueryStep(Exception exception)
        {
            _exception = exception;
        }

        public ISerializeResultStep HandleQuery(Func<IHandleExecutable, IQuery, object> executeQuery)
        {
            return new SerializeResultStep(new QueryResult(_exception));
        }

        public ISerializeResultStep HandleQuery()
        {
            return new SerializeResultStep(new QueryResult(_exception));
        }
    }
}