using System;
using Qvc.Executables;
using Qvc.Handlers;
using Qvc.Results;

namespace Qvc.Steps.Implementations
{
    public class CommandErrorStep :
        IDeserializeCommandStep,
        IFindCommandHandlerStep,
        ICreateCommandHandlerStep,
        IExecuteCommandStep
    {
        public CommandResult CommandResult { get; private set; }

        public CommandErrorStep(CommandResult commandResult)
        {
            CommandResult = commandResult;
        }

        public IFindCommandHandlerStep DeserializeCommand(Func<string, Type, object> deserializeTheCommand)
        {
            return this;
        }

        public IFindCommandHandlerStep DeserializeCommand()
        {
            return this;
        }

        public ICreateCommandHandlerStep FindCommandHandler(Func<ICommand, Type> findCommandHandler)
        {
            return this;
        }

        public IExecuteCommandStep CreateCommandHandler(Func<Type, object> createCommandHandler)
        {
            return this;
        }

        public IExecuteCommandStep CreateCommandHandler()
        {
            return this;
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