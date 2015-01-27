using System;
using Qvc.Executables;
using Qvc.Handlers;
using Qvc.Results;

namespace Qvc.Steps.Implementations
{
    public class QueryErrorStep :
        IJsonAndQueryType,
        IFindQueryHandlerStep,
        ICreateQueryHandlerStep,
        IExecuteQueryStep
    {
        public QueryResult QueryResult { get; private set; }

        public QueryErrorStep(QueryResult queryResult)
        {
            QueryResult = queryResult;
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
            return new SerializeResultStep(QueryResult);
        }

        public ISerializeResultStep HandleQuery()
        {
            return new SerializeResultStep(QueryResult);
        }
    }
}