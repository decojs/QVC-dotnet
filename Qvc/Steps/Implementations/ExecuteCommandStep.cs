using System;
using System.Reflection;
using Qvc.Executables;
using Qvc.Handlers;
using Qvc.Results;

namespace Qvc.Steps.Implementations
{
    public class ExecuteCommandStep : IExecuteCommandStep
    {
        public ICommand Command { get; private set; }

        public IHandleExecutable Handler { get; private set; }

        public ExecuteCommandStep(ICommand command, IHandleExecutable handler)
        {
            Command = command;
            Handler = handler;
        }

        public ISerializeResultStep HandleCommand(Action<IHandleExecutable, ICommand> executeCommand)
        {
            try
            {
                executeCommand.Invoke(Handler, Command);
                return new SerializeResultStep(new CommandResult());
            }
            catch (TargetInvocationException e)
            {
                return new SerializeResultStep(new CommandResult(e.GetBaseException()));
            }
            catch (Exception e)
            {
                return new SerializeResultStep(new CommandResult(e));
            }
        }

        public ISerializeResultStep HandleCommand()
        {
            return HandleCommand(Default.HandleCommand);
        }
    }
}