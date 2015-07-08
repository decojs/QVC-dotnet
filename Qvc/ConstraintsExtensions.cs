using System;
using System.Threading.Tasks;

using Qvc.Results;
using Qvc.Steps;

namespace Qvc
{
    public static class ConstraintsExtensions
    {
        public static Task<Type> ThenFindExecutable(this Task<string> executableName, Func<string, Type> getExecutable)
        {
            return executableName.Then(getExecutable);
        }

        public static Task<ConstraintsResult> ThenGetConstraints(
            this Task<Type> executableType,
            Func<Type, ConstraintsResult> getConstraints)
        {
            return executableType.Then(getConstraints);
        }

        public static Task<ConstraintsResult> ThenGetConstraints(
            this Task<Type> executableType,
            Func<Type, Task<ConstraintsResult>> getConstraintsAsync)
        {
            return executableType.Then(getConstraintsAsync);
        }

        public static Task<string> ThenSerialize(this Task<ConstraintsResult> self, Func<ConstraintsResult, string> serializeResult)
        {
            return self
                .Catch(ConstraintsSteps.ExceptionToConstraintsResult)
                .Then(serializeResult);
        }

        public static Task<string> ThenSerialize(this Task<ConstraintsResult> self)
        {
            return ThenSerialize(self, Default.Serialize);
        }
    }
}