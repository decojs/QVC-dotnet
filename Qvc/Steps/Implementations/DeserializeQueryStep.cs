using System;
using Qvc.Executables;

namespace Qvc.Steps.Implementations
{
    public class DeserializeQueryStep : IDeserializeQueryStep
    {
        private readonly string _json;
        private readonly Type _type;

        public DeserializeQueryStep(string json, Type type)
        {
            _json = json;
            _type = type;
        }

        public IFindQueryHandlerStep DeserializeQuery(Func<string, Type, object> deserializeTheQuery)
        {
            var executable = deserializeTheQuery.Invoke(_json, _type) as IQuery;
            return new FindQueryHandlerStep(executable);
        }

        public IFindQueryHandlerStep DeserializeQuery()
        {
            return DeserializeQuery(Default.Deserialize);
        }
    }
}