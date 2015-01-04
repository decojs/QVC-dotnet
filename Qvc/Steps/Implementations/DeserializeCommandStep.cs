using System;
using Qvc.Executables;

namespace Qvc.Steps.Implementations
{
    public class DeserializeCommandStep : IDeserializeCommandStep
    {
        public string Json { get; private set; }

        public Type Type { get; private set; }

        public DeserializeCommandStep(string json, Type type)
        {
            Json = json;
            Type = type;
        }

        public IFindCommandHandlerStep DeserializeCommand(Func<string, Type, object> deserializeTheCommand)
        {
            var executable = deserializeTheCommand.Invoke(Json, Type) as ICommand;
            return new FindCommandHandlerStep(executable);
        }

        public IFindCommandHandlerStep DeserializeCommand()
        {
            return DeserializeCommand(Default.Deserialize);
        }
    }
}