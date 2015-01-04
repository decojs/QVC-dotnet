using System;
using Qvc.Executables;

namespace Qvc.Steps.Implementations
{
    public class DeserializeQueryStep : IDeserializeQueryStep
    {
        public string Json { get; private set; }

        public Type Type { get; private set; }

        public DeserializeQueryStep(string json, Type type)
        {
            Json = json;
            Type = type;
        }

        public IFindQueryHandlerStep DeserializeQuery(Func<string, Type, object> deserializeTheQuery)
        {
            var executable = deserializeTheQuery.Invoke(Json, Type) as IQuery;
            return new FindQueryHandlerStep(executable);
        }

        public IFindQueryHandlerStep DeserializeQuery()
        {
            return DeserializeQuery(Default.Deserialize);
        }
    }
}