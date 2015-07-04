using System;

namespace Qvc.Steps
{
    public class JsonAndType
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