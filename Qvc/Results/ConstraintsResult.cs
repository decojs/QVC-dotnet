using System.Collections.Generic;
using Qvc.Constraints;

namespace Qvc.Results
{
    public class ConstraintsResult
    {
        public ConstraintsResult(IReadOnlyCollection<Parameter> parameters)
        {
            Parameters = parameters;
        }

        public IReadOnlyCollection<Parameter> Parameters { get; private set; }
    }
}