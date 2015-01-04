using System;
using Qvc.Exceptions;
using Qvc.Executables;
using Qvc.Results;

namespace Qvc.Steps.Implementations
{
    public class FindCommandHandlerStep : IFindCommandHandlerStep
    {
        public ICommand Command { get; private set; }

        public FindCommandHandlerStep(ICommand command)
        {
            Command = command;
        }

        public ICreateCommandHandlerStep FindCommandHandler(Func<ICommand, Type> findCommandHandler)
        {
            try
            {
                var handlerType = findCommandHandler.Invoke(Command);
                return new CreateCommandHandlerStep(Command, handlerType);
            }
            catch (CommandHandlerDoesNotExistException e)
            {
                return new CommandErrorStep(new CommandResult(e));
            }
            catch (DuplicateCommandHandlerException e)
            {
                return new CommandErrorStep(new CommandResult(e));
            }
        }
    }
}