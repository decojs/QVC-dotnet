using System;
using Qvc.Executables;

namespace Qvc.Steps
{
    public class CommandAndHandlerType
    {
        public ICommand Command { get; private set; }

        public Type HandlerType { get; private set; }

        public CommandAndHandlerType(ICommand command, Type handlerType)
        {
            Command = command;
            HandlerType = handlerType;
        }
    }
}