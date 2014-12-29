using System;

namespace Qvc.Steps
{
    public interface ICreateCommandHandlerStep
    {
        IExecuteCommandStep CreateCommandHandler(Func<Type, object> createCommandHandler);

        IExecuteCommandStep CreateCommandHandler();
    }
}