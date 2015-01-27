using System;
using Qvc.Exceptions;
using Qvc.Results;
using Qvc.Steps;
using Qvc.Steps.Implementations;

namespace Qvc
{
    public static class QuerySteps
    {
        public static IDeserializeQueryStep FindQuery(this QueryNameAndJson queryNameAndJson, Func<string, Type> getQuery)
        {
            try
            {
                var type = getQuery.Invoke(queryNameAndJson.Name);
                return new DeserializeQueryStep(queryNameAndJson.Json, type);
            }
            catch (QueryDoesNotExistException e)
            {
                return new QueryErrorStep(new QueryResult(e));
            }
            catch (ExecutableDoesNotExistException e)
            {
                return new QueryErrorStep(new QueryResult(e));
            }
        }
    }
}