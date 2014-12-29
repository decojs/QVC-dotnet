using System;
using System.Reflection;
using Qvc.Executables;
using Qvc.Handlers;
using Qvc.Results;

namespace Qvc.Steps
{
    public class ExecuteQueryStep
    {
        private readonly IQuery _query;
        private readonly IHandleExecutable _handler;

        public ExecuteQueryStep(IQuery query, IHandleExecutable handler)
        {
            _query = query;
            _handler = handler;
        }
        
        public SerializeResultStep HandleQuery(Func<IHandleExecutable, IQuery, object> executeQuery)
        {
            try
            {
                var result = executeQuery.Invoke(_handler, _query);
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

        public SerializeResultStep HandleQuery()
        {
            return HandleQuery(Default.HandleQuery);
        }
    }
}