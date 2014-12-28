using System;
using Qvc.Executables;

namespace Qvc.Steps
{
    public class FindCommandHandlerStep
    {
        private readonly ICommand _command;

        public FindCommandHandlerStep(ICommand command)
        {
            _command = command;
        }

        public CreateCommandHandlerStep FindCommandHandler(Func<ICommand, Type> findCommandHandler)
        {
            var handlerType = findCommandHandler.Invoke(_command);
            return new CreateCommandHandlerStep(_command, handlerType);
        }
    }
}