using System;
using Qvc.Executables;

namespace Qvc.Steps
{
    public interface IFindCommandHandlerStep
    {
        ICreateCommandHandlerStep FindCommandHandler(Func<ICommand, Type> findCommandHandler);
    }
}