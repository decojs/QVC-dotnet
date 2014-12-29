using System;

namespace Qvc.Steps.Implementations
{
    public class DontCreateCommandHandlerStep : ICreateCommandHandlerStep
    {
        private readonly Exception _exception;

        public DontCreateCommandHandlerStep(Exception exception)
        {
            _exception = exception;
        }

        public IExecuteCommandStep CreateCommandHandler(Func<Type, object> createCommandHandler)
        {
            return new DontExecuteCommandStep(_exception);
        }

        public IExecuteCommandStep CreateCommandHandler()
        {
            return new DontExecuteCommandStep(_exception);
        }
    }
}