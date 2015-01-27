﻿using System;
using System.Reflection;
using Qvc.Exceptions;
using Qvc.Executables;
using Qvc.Handlers;
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

        public static IQuery DeserializeQuery(this IJsonAndQueryType self, Func<string, Type, object> deserializeTheQuery)
        {
            return self.Virtually<IJsonAndQueryType, IQuery>()
                .Case<JsonAndType>(result => deserializeTheQuery.Invoke(result.Json, result.Type) as IQuery)
                .Case<QueryErrorStep>(error => error)
                .Result();
        }

        public static IQuery DeserializeQuery(this IJsonAndQueryType self)
        {
            return DeserializeQuery(self, Default.Deserialize);
        }
        
        public static IQueryAndHandlerType FindQueryHandler(this IQuery self, Func<IQuery, Type> findQueryHandler)
        {
            return self.Virtually<IQuery, IQueryAndHandlerType>()
                .Case<QueryErrorStep>(error => error)
                .Default(query =>
                {
                    try
                    {
                        var handlerType = findQueryHandler.Invoke(query);
                        return new QueryAndHandlerType(query, handlerType);
                    }
                    catch (QueryHandlerDoesNotExistException e)
                    {
                        return new QueryErrorStep(new QueryResult(e));
                    }
                    catch (DuplicateQueryHandlerException e)
                    {
                        return new QueryErrorStep(new QueryResult(e));
                    }
                })
                .Result();
        }

        public static IQueryAndHandler CreateQueryHandler(this IQueryAndHandlerType self, Func<Type, object> createQueryHandler)
        {
            return self.Virtually<IQueryAndHandlerType, IQueryAndHandler>()
                .Case<QueryAndHandlerType>(result =>
                {
                    try
                    {
                        var handler = createQueryHandler.Invoke(result.HandlerType);
                        return new QueryAndHandler(result.Query, handler as IHandleExecutable);
                    }
                    catch (Exception e)
                    {
                        return new QueryErrorStep(new QueryResult(e));
                    }
                })
                .Case<QueryErrorStep>(error => error)
                .Result();
            
        }

        public static IQueryAndHandler CreateQueryHandler(this IQueryAndHandlerType self)
        {
            return CreateQueryHandler(self, Default.CreateHandler);
        }

        public static ISerializeResultStep HandleQuery(this IQueryAndHandler self, Func<IHandleExecutable, IQuery, object> executeQuery)
        {
            return self.Virtually<IQueryAndHandler, ISerializeResultStep>()
                .Case<QueryAndHandler>(queryAndHandler =>
                {
                    try
                    {
                        var result = executeQuery.Invoke(queryAndHandler.Handler, queryAndHandler.Query);
                        return new SerializeResultStep(new QueryResult(result));
                    }
                    catch (TargetInvocationException e)
                    {
                        return new SerializeResultStep(new QueryResult(e.GetBaseException()));
                    }
                    catch (Exception e)
                    {
                        return new SerializeResultStep(new QueryResult(e));
                    }
                })
                .Case<QueryErrorStep>(error => new SerializeResultStep(error.QueryResult))
                .Result();
        }

        public static ISerializeResultStep HandleQuery(this IQueryAndHandler self)
        {
            return HandleQuery(self, Default.HandleQuery);
        }
    }
}