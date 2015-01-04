using System;
using Qvc.Executables;
using Qvc.Handlers;

namespace Qvc.Steps.Implementations
{
    public class CreateCommandHandlerStep : ICreateCommandHandlerStep
    {
        public ICommand Command { get; private set; }

        public Type HandlerType { get; private set; }

        public CreateCommandHandlerStep(ICommand command, Type handlerType)
        {
            Command = command;
            HandlerType = handlerType;
        }

        public IExecuteCommandStep CreateCommandHandler(Func<Type, object> createCommandHandler)
        {
            try
            {
                var handler = createCommandHandler.Invoke(HandlerType);
                return new ExecuteCommandStep(Command, handler as IHandleExecutable);
            }
            catch (Exception e)
            {
                return new ErrorStep(e);
            }
        }

        public IExecuteCommandStep CreateCommandHandler()
        {
            return CreateCommandHandler(Default.CreateHandler);
        }
    }
}