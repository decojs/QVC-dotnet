using System;

namespace Qvc.Steps
{
    public interface IGetExecutableStep
    {
        IDeserializeCommandStep GetCommand(Func<string, Type> getCommand);

        IDeserializeQueryStep GetQuery(Func<string, Type> getQuery);
    }
}