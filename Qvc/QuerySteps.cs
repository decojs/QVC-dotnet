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
    public static class QuerySteps
    {
        public static JsonAndType FindQuery(QueryNameAndJson queryNameAndJson, Func<string, Type> getQuery)
        {
            var type = getQuery.Invoke(queryNameAndJson.Name);
            return new JsonAndType(queryNameAndJson.Json, type);
        }

        public static IQuery DeserializeQuery(JsonAndType self, Func<string, Type, object> deserializeTheQuery)
        {
            return deserializeTheQuery.Invoke(self.Json, self.Type) as IQuery;
        }

        public static IQuery DeserializeQuery(JsonAndType self)
        {
            return DeserializeQuery(self, Default.Deserialize);
        }

        public static IQuery ValidateQuery(IQuery query)
        {
            Validator.Validate(query);
            return query;
        }
        
        public static QueryAndHandlerType FindQueryHandler(IQuery self, Func<IQuery, Type> findQueryHandler)
        {
            var handlerType = findQueryHandler.Invoke(self);
            return new QueryAndHandlerType(self, handlerType);
        }

        public static QueryAndHandler CreateQueryHandler(QueryAndHandlerType self, Func<Type, object> createQueryHandler)
        {
            var handler = createQueryHandler.Invoke(self.HandlerType);
            return new QueryAndHandler(self.Query, handler as IHandleExecutable);
        }

        public static QueryAndHandler CreateQueryHandler(QueryAndHandlerType self)
        {
            return CreateQueryHandler(self, Default.CreateHandler);
        }

        public static async Task<QueryResult> HandleQuery(QueryAndHandler self, Func<IHandleExecutable, IQuery, Task<object>> executeQuery)
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
        }

        public static Task<QueryResult> HandleQuery(QueryAndHandler self)
        {
            return HandleQuery(self, Default.HandleQuery);
        }

        public static QueryResult ExceptionToQueryResult(Exception exception)
        {
            if (exception is ValidationException)
            {
                return new QueryResult(exception as ValidationException);
            }

            return new QueryResult(exception);
        }

        public static string Serialize(QueryResult self, Func<QueryResult, string> serializeResult)
        {
            return serializeResult.Invoke(self);
        }

        public static string Serialize(QueryResult self)
        {
            return Serialize(self, Default.Serialize);
        }
    }
}