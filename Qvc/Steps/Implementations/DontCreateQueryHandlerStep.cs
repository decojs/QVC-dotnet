using System;

namespace Qvc.Steps.Implementations
{
    public class DontCreateQueryHandlerStep : ICreateQueryHandlerStep
    {
        private readonly Exception _exception;

        public DontCreateQueryHandlerStep(Exception exception)
        {
            _exception = exception;
        }

        public IExecuteQueryStep CreateQueryHandler(Func<Type, object> createQueryHandler)
        {
            return new DontExecuteQueryStep(_exception);
        }

        public IExecuteQueryStep CreateQueryHandler()
        {
            return new DontExecuteQueryStep(_exception);
        }
    }
}