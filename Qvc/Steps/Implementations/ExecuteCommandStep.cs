using System;
using System.Reflection;
using Qvc.Executables;
using Qvc.Handlers;
using Qvc.Results;

namespace Qvc.Steps.Implementations
{
    public class ExecuteCommandStep : IExecuteCommandStep
    {
        private readonly ICommand _command;
        private readonly IHandleExecutable _handler;

        public ExecuteCommandStep(ICommand command, IHandleExecutable handler)
        {
            _command = command;
            _handler = handler;
        }

        public ISerializeResultStep HandleCommand(Action<IHandleExecutable, ICommand> executeCommand)
        {
            try
            {
                executeCommand.Invoke(_handler, _command);
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