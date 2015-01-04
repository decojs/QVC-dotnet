using System;
using Qvc.Executables;
using Qvc.Handlers;
using Qvc.Results;

namespace Qvc.Steps.Implementations
{
    public class CreateQueryHandlerStep : ICreateQueryHandlerStep
    {
        public IQuery Query { get; private set; }

        public Type HandlerType { get; private set; }

        public CreateQueryHandlerStep(IQuery query, Type handlerType)
        {
            Query = query;
            HandlerType = handlerType;
        }

        public IExecuteQueryStep CreateQueryHandler(Func<Type, object> createQueryHandler)
        {
            try
            {
                var handler = createQueryHandler.Invoke(HandlerType);
                return new ExecuteQueryStep(Query, handler as IHandleExecutable);
            }
            catch (Exception e)
            {
                return new QueryErrorStep(new QueryResult(e));
            }
        }

        public IExecuteQueryStep CreateQueryHandler()
        {
            return CreateQueryHandler(Default.CreateHandler);
        }
    }
}