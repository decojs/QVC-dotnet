using System;
using Qvc.Executables;

namespace Qvc.Steps.Implementations
{
    public class DontFindCommandHandlerStep : IFindCommandHandlerStep
    {
        private readonly Exception _exception;

        public DontFindCommandHandlerStep(Exception exception)
        {
            _exception = exception;
        }

        public ICreateCommandHandlerStep FindCommandHandler(Func<ICommand, Type> findCommandHandler)
        {
            return new DontCreateCommandHandlerStep(_exception);
        }
    }
}