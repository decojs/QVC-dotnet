using System;
using System.Reflection;
using Qvc.Executables;
using Qvc.Handlers;
using Qvc.Results;

namespace Qvc.Steps.Implementations
{
    public class ExecuteQueryStep : IExecuteQueryStep
    {
        public IQuery Query { get; private set; }

        public IHandleExecutable Handler { get; private set; }

        public ExecuteQueryStep(IQuery query, IHandleExecutable handler)
        {
            Query = query;
            Handler = handler;
        }
        
        public ISerializeResultStep HandleQuery(Func<IHandleExecutable, IQuery, object> executeQuery)
        {
            try
            {
                var result = executeQuery.Invoke(Handler, Query);
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
        }

        public ISerializeResultStep HandleQuery()
        {
            return HandleQuery(Default.HandleQuery);
        }
    }
}