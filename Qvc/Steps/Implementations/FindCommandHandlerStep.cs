using System;
using Qvc.Executables;

namespace Qvc.Steps.Implementations
{
    public class FindCommandHandlerStep : IFindCommandHandlerStep
    {
        private readonly ICommand _command;

        public FindCommandHandlerStep(ICommand command)
        {
            _command = command;
        }

        public ICreateCommandHandlerStep FindCommandHandler(Func<ICommand, Type> findCommandHandler)
        {
            var handlerType = findCommandHandler.Invoke(_command);
            return new CreateCommandHandlerStep(_command, handlerType);
        }
    }
}