using System;

namespace Qvc.Results
{
    public class ExecutableResult
    {
        public bool Success { get; protected set; }

        public bool Valid { get; protected set; }

        public Exception Exception { get; protected set; }
    }
}