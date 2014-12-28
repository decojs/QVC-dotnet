using System;
using Qvc.Executables;

namespace Qvc.Steps
{
    public class DeserializeCommandStep
    {
        private readonly string _json;
        private readonly Type _type;

        public DeserializeCommandStep(string json, Type type)
        {
            _json = json;
            _type = type;
        }

        public FindCommandHandlerStep DeserializeCommand(Func<string, Type, object> deserializeTheCommand)
        {
            var executable = deserializeTheCommand.Invoke(_json, _type) as ICommand;
            return new FindCommandHandlerStep(executable);
        }
    }
}