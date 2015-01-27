using Qvc.Executables;
using Qvc.Handlers;

namespace Qvc.Steps.Implementations
{
    public class CommandAndHandler : ICommandAndHandler
    {
        public ICommand Command { get; private set; }

        public IHandleExecutable Handler { get; private set; }

        public CommandAndHandler(ICommand command, IHandleExecutable handler)
        {
            Command = command;
            Handler = handler;
        }
    }
}