using System;
using Qvc.Executables;

namespace Qvc.Steps
{
    public class DeserializeQueryStep
    {
        private readonly string _json;
        private readonly Type _type;

        public DeserializeQueryStep(string json, Type type)
        {
            _json = json;
            _type = type;
        }

        public FindQueryHandlerStep DeserializeQuery(Func<string, Type, object> deserializeTheQuery)
        {
            var executable = deserializeTheQuery.Invoke(_json, _type) as IQuery;
            return new FindQueryHandlerStep(executable);
        }
    }
}