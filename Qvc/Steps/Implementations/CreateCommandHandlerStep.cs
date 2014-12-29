using System;
using Qvc.Executables;
using Qvc.Handlers;

namespace Qvc.Steps.Implementations
{
    public class CreateCommandHandlerStep : ICreateCommandHandlerStep
    {
        private readonly ICommand _command;
        private readonly Type _handlerType;

        public CreateCommandHandlerStep(ICommand command, Type handlerType)
        {
            _command = command;
            _handlerType = handlerType;
        }

        public IExecuteCommandStep CreateCommandHandler(Func<Type, object> createCommandHandler)
        {
            try
            {
                var handler = createCommandHandler.Invoke(_handlerType);
                return new ExecuteCommandStep(_command, handler as IHandleExecutable);
            }
            catch (Exception e)
            {
                return new DontExecuteCommandStep(e);
            }
        }

        public IExecuteCommandStep CreateCommandHandler()
        {
            return CreateCommandHandler(Default.CreateHandler);
        }
    }
}