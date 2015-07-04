using System;
using Qvc.Validation;

namespace Qvc.Results
{
    public class CommandResult : ExecutableResult
    {
        public CommandResult()
        {
            Success = true;
            Valid = true;
        }

        public CommandResult(Exception exception)
        {
            Success = false;
            Valid = true;
            Exception = exception;
        }

        public CommandResult(ValidationException exception)
        {
            Success = false;
            Valid = false;
            Violations = exception.Violations;
        }
    }
}