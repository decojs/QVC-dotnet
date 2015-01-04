using System;
using Qvc.Executables;
using Qvc.Handlers;
using Qvc.Results;

namespace Qvc.Steps.Implementations
{
    public class QueryErrorStep :
        IGetQueryStep,
        IDeserializeQueryStep,
        IFindQueryHandlerStep,
        ICreateQueryHandlerStep,
        IExecuteQueryStep
    {
        public Exception Exception { get; private set; }

        public QueryErrorStep(Exception exception)
        {
            Exception = exception;
        }

        public IDeserializeQueryStep GetQuery(Func<string, Type> getQuery)
        {
            return this;
        }

        public IFindQueryHandlerStep DeserializeQuery(Func<string, Type, object> deserializeTheQuery)
        {
            return this;
        }

        public IFindQueryHandlerStep DeserializeQuery()
        {
            return this;
        }

        public ICreateQueryHandlerStep FindQueryHandler(Func<IQuery, Type> findQueryHandler)
        {
            return this;
        }

        public IExecuteQueryStep CreateQueryHandler(Func<Type, object> createQueryHandler)
        {
            return this;
        }

        public IExecuteQueryStep CreateQueryHandler()
        {
            return this;
        }

        public ISerializeResultStep HandleQuery(Func<IHandleExecutable, IQuery, object> executeQuery)
        {
            return new SerializeResultStep(new QueryResult(Exception));
        }

        public ISerializeResultStep HandleQuery()
        {
            return new SerializeResultStep(new QueryResult(Exception));
        }
    }
}