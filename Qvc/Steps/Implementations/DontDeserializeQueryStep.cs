using System;

namespace Qvc.Steps.Implementations
{
    public class DontDeserializeQueryStep : IDeserializeQueryStep
    {
        private readonly Exception _exception;

        public DontDeserializeQueryStep(Exception exception)
        {
            _exception = exception;
        }

        public IFindQueryHandlerStep DeserializeQuery(Func<string, Type, object> deserializeTheQuery)
        {
            return new DontFindQueryHandlerStep(_exception);
        }

        public IFindQueryHandlerStep DeserializeQuery()
        {
            return new DontFindQueryHandlerStep(_exception);
        }
    }
}