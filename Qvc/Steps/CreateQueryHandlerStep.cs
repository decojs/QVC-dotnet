using System;
using Qvc.Executables;
using Qvc.Handlers;

namespace Qvc.Steps
{
    public class CreateQueryHandlerStep
    {
        private readonly IQuery _query;
        private readonly Type _handlerType;

        public CreateQueryHandlerStep(IQuery query, Type handlerType)
        {
            _query = query;
            _handlerType = handlerType;
        }

        public ExecuteQueryStep CreateQueryHandler(Func<Type, object> createQueryHandler)
        {
            var handler = createQueryHandler.Invoke(_handlerType);
            return new ExecuteQueryStep(_query, handler as IHandleExecutable);
        }

        public ExecuteQueryStep CreateQueryHandler()
        {
            return CreateQueryHandler(Default.CreateHandler);
        }
    }
}