using System;
using System.Reflection;
using Qvc.Executables;
using Qvc.Handlers;
using Qvc.Results;

namespace Qvc.Steps
{
    public class ExecuteCommandStep
    {
        private readonly ICommand _command;
        private readonly IHandleExecutable _handler;

        public ExecuteCommandStep(ICommand command, IHandleExecutable handler)
        {
            _command = command;
            _handler = handler;
        }

        public SerializeResultStep HandleCommand(Action<IHandleExecutable, ICommand> executeCommand)
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

        public SerializeResultStep HandleCommand()
        {
            return HandleCommand(Default.HandleCommand);
        }
    }
}