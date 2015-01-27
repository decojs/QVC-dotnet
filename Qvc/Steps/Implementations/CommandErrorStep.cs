using Qvc.Executables;
using Qvc.Results;

namespace Qvc.Steps.Implementations
{
    public class CommandErrorStep :
        IJsonAndCommandType,
        ICommand,
        ICommandAndHandlerType,
        ICommandAndHandler
    {
        public CommandResult CommandResult { get; private set; }

        public CommandErrorStep(CommandResult commandResult)
        {
            CommandResult = commandResult;
        }
    }
}