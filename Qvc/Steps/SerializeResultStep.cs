using System;
using Qvc.Results;

namespace Qvc.Steps
{
    public class SerializeResultStep
    {
        private readonly ExecutableResult _result;

        public SerializeResultStep(ExecutableResult result)
        {
            _result = result;
        }

        public string Serialize(Func<ExecutableResult, string> serializeResult)
        {
            return serializeResult.Invoke(_result);
        }

        public string Serialize()
        {
            return Serialize(Default.Serialize);
        }
    }
}