using System;
using Qvc.Exceptions;
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
            try
            {
                var handlerType = findCommandHandler.Invoke(_command);
                return new CreateCommandHandlerStep(_command, handlerType);
            }
            catch (CommandHandlerDoesNotExistException e)
            {
                return new ErrorStep(e);
            }
            catch (DuplicateCommandHandlerException e)
            {
                return new ErrorStep(e);
            }
        }
    }
}