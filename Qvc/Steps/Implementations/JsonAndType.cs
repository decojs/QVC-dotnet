using System;

namespace Qvc.Steps.Implementations
{
    public class JsonAndType : IJsonAndCommandType, IJsonAndQueryType
    {
        public string Json { get; private set; }

        public Type Type { get; private set; }

        public JsonAndType(string json, Type type)
        {
            Json = json;
            Type = type;
        }
    }
}