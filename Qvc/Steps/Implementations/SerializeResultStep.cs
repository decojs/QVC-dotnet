using System;
using Qvc.Results;

namespace Qvc.Steps.Implementations
{
    public class SerializeResultStep : ISerializeResultStep
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