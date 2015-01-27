using System;
using Qvc.Executables;
using Qvc.Handlers;
using Qvc.Results;

namespace Qvc.Steps.Implementations
{
    public class CommandErrorStep :
        IJsonAndCommandType,
        ICommand,
        ICommandAndHandlerType,
        IExecuteCommandStep
    {
        public CommandResult CommandResult { get; private set; }

        public CommandErrorStep(CommandResult commandResult)
        {
            CommandResult = commandResult;
        }

        public ISerializeResultStep HandleCommand(Action<IHandleExecutable, ICommand> executeCommand)
        {
            return new SerializeResultStep(CommandResult);
        }

        public ISerializeResultStep HandleCommand()
        {
            return new SerializeResultStep(CommandResult);
        }
    }
}