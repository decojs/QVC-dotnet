using System;
using System.Collections.Generic;

using Qvc.Constraints;
using Qvc.Results;

namespace Qvc.Steps
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

        public static ConstraintsResult ExceptionToConstraintsResult(Exception e)
        {
            return new ConstraintsResult(new List<Parameter>());
        }
    }
}