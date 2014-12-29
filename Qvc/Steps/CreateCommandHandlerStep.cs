using System;
using Qvc.Executables;
using Qvc.Handlers;

namespace Qvc.Steps
{
    public class CreateCommandHandlerStep
    {
        private readonly ICommand _command;
        private readonly Type _handlerType;

        public CreateCommandHandlerStep(ICommand command, Type handlerType)
        {
            _command = command;
            _handlerType = handlerType;
        }

        public ExecuteCommandStep CreateCommandHandler(Func<Type, object> createCommandHandler)
        {
            var handler = createCommandHandler.Invoke(_handlerType);
            return new ExecuteCommandStep(_command, handler as IHandleExecutable);
        }

        public ExecuteCommandStep CreateCommandHandler()
        {
            return CreateCommandHandler(Default.CreateHandler);
        }
    }
}