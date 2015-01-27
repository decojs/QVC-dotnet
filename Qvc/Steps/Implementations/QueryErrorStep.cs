using System;
using Qvc.Executables;
using Qvc.Handlers;
using Qvc.Results;

namespace Qvc.Steps.Implementations
{
    public class QueryErrorStep :
        IJsonAndQueryType,
        IQuery,
        IQueryAndHandlerType,
        IExecuteQueryStep
    {
        public QueryResult QueryResult { get; private set; }

        public QueryErrorStep(QueryResult queryResult)
        {
            QueryResult = queryResult;
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