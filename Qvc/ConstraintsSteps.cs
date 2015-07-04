using System;
using Qvc.Results;

namespace Qvc
{
    public static class ConstraintsSteps
    {
        public static Type FindExecutable(string executableName, Func<string, Type> getExecutableType)
        {
            return getExecutableType.Invoke(executableName);
        }

        public static ConstraintsResult GetConstraints(Type executable)
        {
            return new ConstraintsResult(null);
        }

        public static string Serialize(ConstraintsResult self, Func<ConstraintsResult, string> serializeResult)
        {
            return serializeResult.Invoke(self);
        }

        public static string Serialize(ConstraintsResult self)
        {
            return Serialize(self, Default.Serialize);
        }
    }
}