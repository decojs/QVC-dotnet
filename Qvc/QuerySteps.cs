using System;
using Qvc.Exceptions;
using Qvc.Executables;
using Qvc.Results;
using Qvc.Steps;
using Qvc.Steps.Implementations;

namespace Qvc
{
    public static class QuerySteps
    {
        public static IJsonAndQueryType FindQuery(this QueryNameAndJson queryNameAndJson, Func<string, Type> getQuery)
        {
            try
            {
                var type = getQuery.Invoke(queryNameAndJson.Name);
                return new JsonAndType(queryNameAndJson.Json, type);
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

        public static IFindQueryHandlerStep DeserializeQuery(this IJsonAndQueryType self, Func<string, Type, object> deserializeTheQuery)
        {
            return self.Virtually<IJsonAndQueryType, IFindQueryHandlerStep>()
                .Case<JsonAndType>(result => new FindQueryHandlerStep(deserializeTheQuery.Invoke(result.Json, result.Type) as IQuery))
                .Case<QueryErrorStep>(error => new QueryErrorStep(error.QueryResult))
                .Result();
        }

        public static IFindQueryHandlerStep DeserializeQuery(this IJsonAndQueryType self)
        {
            return DeserializeQuery(self, Default.Deserialize);
        }
    }
}