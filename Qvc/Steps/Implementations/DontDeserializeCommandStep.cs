using System;

namespace Qvc.Steps.Implementations
{
    public class DontDeserializeCommandStep : IDeserializeCommandStep
    {
        private readonly Exception _exception;

        public DontDeserializeCommandStep(Exception exception)
        {
            _exception = exception;
        }

        public IFindCommandHandlerStep DeserializeCommand(Func<string, Type, object> deserializeTheCommand)
        {
            return new DontFindCommandHandlerStep(_exception);
        }

        public IFindCommandHandlerStep DeserializeCommand()
        {
            return new DontFindCommandHandlerStep(_exception);
        }
    }
}