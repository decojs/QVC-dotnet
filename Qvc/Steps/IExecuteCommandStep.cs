using System;
using Qvc.Executables;
using Qvc.Handlers;

namespace Qvc.Steps
{
    public interface IExecuteCommandStep
    {
        ISerializeResultStep HandleCommand(Action<IHandleExecutable, ICommand> executeCommand);

        ISerializeResultStep HandleCommand();
    }
}