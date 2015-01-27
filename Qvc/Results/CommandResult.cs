using System;
using Qvc.Executables;
using Qvc.Steps;

namespace Qvc.Results
{
    public class CommandResult : ExecutableResult, IJsonAndCommandType, ICommand, ICommandAndHandlerType, ICommandAndHandler
    {
        public CommandResult()
        {
            Success = true;
            Valid = true;
            Exception = null;
        }

        public CommandResult(Exception exception)
        {
            Success = false;
            Valid = true;
            Exception = exception;
        }
    }
}