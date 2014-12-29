using System;

namespace Qvc.Steps
{
    public interface IDeserializeCommandStep
    {
        IFindCommandHandlerStep DeserializeCommand(Func<string, Type, object> deserializeTheCommand);

        IFindCommandHandlerStep DeserializeCommand();
    }
}