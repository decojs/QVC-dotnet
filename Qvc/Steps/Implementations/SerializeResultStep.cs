using System;
using Qvc.Results;

namespace Qvc.Steps.Implementations
{
    public class SerializeResultStep : ISerializeResultStep
    {
        public ExecutableResult Result { get; private set; }

        public SerializeResultStep(ExecutableResult result)
        {
            Result = result;
        }

        public string Serialize(Func<ExecutableResult, string> serializeResult)
        {
            return serializeResult.Invoke(Result);
        }

        public string Serialize()
        {
            return Serialize(Default.Serialize);
        }
    }
}