using System;
using System.Collections.Generic;
using Qvc.Validation;

namespace Qvc.Results
{
    public class ExecutableResult
    {
        public bool Success { get; protected set; }

        public bool Valid { get; protected set; }

        public Exception Exception { get; protected set; }

        public IReadOnlyCollection<Violation> Violations { get; protected set; }

        public ExecutableResult()
        {
            Violations = new List<Violation>();
        }
    }
}