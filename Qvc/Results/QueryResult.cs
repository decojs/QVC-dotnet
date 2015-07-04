using System;
using Qvc.Validation;

namespace Qvc.Results
{
    public class QueryResult : ExecutableResult
    {
        public object Result { get; private set; }

        public QueryResult(object result)
        {
            Success = true;
            Valid = true;
            Result = result;
        }

        public QueryResult(Exception exception)
        {
            Success = false;
            Valid = true;
            Exception = exception;
        }

        public QueryResult(ValidationException exception)
        {
            Success = false;
            Valid = false;
            Violations = exception.Violations;
        }
    }
}