using System;
using Qvc.Executables;
using Qvc.Handlers;
using Qvc.Results;

namespace Qvc.Steps.Implementations
{
    public class DontExecuteCommandStep : IExecuteCommandStep
    {
        private readonly Exception _exception;

        public DontExecuteCommandStep(Exception exception)
        {
            _exception = exception;
        }

        public ISerializeResultStep HandleCommand(Action<IHandleExecutable, ICommand> executeCommand)
        {
            return new SerializeResultStep(new CommandResult(_exception));
        }

        public ISerializeResultStep HandleCommand()
        {
            return new SerializeResultStep(new CommandResult(_exception));
        }
    }
}