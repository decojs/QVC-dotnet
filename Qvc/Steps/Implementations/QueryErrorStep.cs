using Qvc.Executables;
using Qvc.Results;

namespace Qvc.Steps.Implementations
{
    public class QueryErrorStep :
        IJsonAndQueryType,
        IQuery,
        IQueryAndHandlerType,
        IQueryAndHandler
    {
        public QueryResult QueryResult { get; private set; }

        public QueryErrorStep(QueryResult queryResult)
        {
            QueryResult = queryResult;
        }
    }
}