﻿using System;

namespace Qvc.Results
{
    public class QueryResult : ExecutableResult
    {
        public object Result { get; private set; }

        public QueryResult(object result)
        {
            Success = true;
            Valid = true;
            Exception = null;
            Result = result;
        }

        public QueryResult(Exception exception)
        {
            Success = false;
            Valid = true;
            Exception = exception;
            Result = null;
        }
    }
}