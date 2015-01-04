using System;
using Qvc.Executables;
using Qvc.Handlers;

namespace Qvc.Steps.Implementations
{
    public class CreateQueryHandlerStep : ICreateQueryHandlerStep
    {
        private readonly IQuery _query;
        private readonly Type _handlerType;

        public CreateQueryHandlerStep(IQuery query, Type handlerType)
        {
            _query = query;
            _handlerType = handlerType;
        }

        public IExecuteQueryStep CreateQueryHandler(Func<Type, object> createQueryHandler)
        {
            try
            {
                var handler = createQueryHandler.Invoke(_handlerType);
                return new ExecuteQueryStep(_query, handler as IHandleExecutable);
            }
            catch (Exception e)
            {
                return new ErrorStep(e);
            }
        }

        public IExecuteQueryStep CreateQueryHandler()
        {
            return CreateQueryHandler(Default.CreateHandler);
        }
    }
}