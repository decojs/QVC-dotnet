using System;

namespace Qvc.Steps
{
    public interface IGetCommandStep
    {
        IDeserializeCommandStep GetCommand(Func<string, Type> getCommand);
    }
}