using System;

namespace Qvc.Results
{
    public class CommandResult : ExecutableResult
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