using System;
using Qvc.Results;

namespace Qvc.Steps
{
    public interface ISerializeResultStep
    {
        string Serialize(Func<ExecutableResult, string> serializeResult);

        string Serialize();
    }
}