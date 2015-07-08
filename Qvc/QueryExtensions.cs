using System;
using System.Reflection;
using System.Threading.Tasks;

using Qvc.Executables;
using Qvc.Handlers;
using Qvc.Results;
using Qvc.Steps;
using Qvc.Validation;

namespace Qvc
{
    public static class QueryExtensions
    {
        public static Task<JsonAndType> ThenFindQuery(this Task<QueryNameAndJson> queryNameAndJson, Func<string, Type> getQuery)
        {
            return queryNameAndJson.Then(c => new JsonAndType(c.Json, getQuery(c.Name)));
        }

        public static Task<IQuery> ThenDeserializeQuery(this Task<JsonAndType> jsonAndType, Func<string, Type, object> deserializeTheQuery)
        {
            return jsonAndType.Then(self => deserializeTheQuery.Invoke(self.Json, self.Type) as IQuery);
        }

        public static Task<IQuery> ThenDeserializeQuery(this Task<JsonAndType> jsonAndType)
        {
            return ThenDeserializeQuery(jsonAndType, Default.Deserialize);
        }

        public static Task<IQuery> ThenValidateQuery(this Task<IQuery> query)
        {
            return query.Then(c =>
            {
                Validator.Validate(c);
                return c;
            });
        }

        public static Task<QueryAndHandlerType> ThenFindQueryHandler(this Task<IQuery> query, Func<IQuery, Type> findQueryHandler)
        {
            return query.Then(self => new QueryAndHandlerType(self, findQueryHandler.Invoke(self)));
        }

        public static Task<QueryAndHandler> ThenCreateQueryHandler(this Task<QueryAndHandlerType> queryAndHandlerType, Func<Type, object> createQueryHandler)
        {
            return queryAndHandlerType.Then(self => new QueryAndHandler(self.Query, createQueryHandler.Invoke(self.HandlerType) as IHandleExecutable));
        }

        public static Task<QueryAndHandler> ThenCreateQueryHandler(this Task<QueryAndHandlerType> self)
        {
            return ThenCreateQueryHandler(self, Default.CreateHandler);
        }

        public static Task<QueryResult> ThenHandleQuery(this Task<QueryAndHandler> queryAndHandler, Func<IHandleExecutable, IQuery, Task<object>> executeQuery)
        {
            return queryAndHandler.Then(async self =>
            {
                try
                {
                    var result = await executeQuery.Invoke(self.Handler, self.Query);
                    return new QueryResult(result);
                }
                catch (TargetInvocationException e)
                {
                    throw e.GetBaseException();
                }
            });
        }

        public static Task<QueryResult> ThenHandleQuery(this Task<QueryAndHandler> self)
        {
            return ThenHandleQuery(self, Default.HandleQuery);
        }

        public static Task<string> ThenSerializeResult(this Task<QueryResult> queryResult, Func<QueryResult, string> serializeResult)
        {
            return queryResult
                .Catch(QuerySteps.ExceptionToQueryResult)
                .Then(serializeResult);
        }

        public static Task<string> ThenSerializeResult(this Task<QueryResult> self)
        {
            return ThenSerializeResult(self, Default.Serialize);
        } 
    }
}