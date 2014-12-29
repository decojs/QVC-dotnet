using System;
using Qvc.Executables;

namespace Qvc.Steps.Implementations
{
    public class DeserializeCommandStep : IDeserializeCommandStep
    {
        private readonly string _json;
        private readonly Type _type;

        public DeserializeCommandStep(string json, Type type)
        {
            _json = json;
            _type = type;
        }

        public IFindCommandHandlerStep DeserializeCommand(Func<string, Type, object> deserializeTheCommand)
        {
            var executable = deserializeTheCommand.Invoke(_json, _type) as ICommand;
            return new FindCommandHandlerStep(executable);
        }

        public IFindCommandHandlerStep DeserializeCommand()
        {
            return DeserializeCommand(Default.Deserialize);
        }
    }
}