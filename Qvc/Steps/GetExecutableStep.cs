using System;

namespace Qvc.Steps
{
    public class GetExecutableStep
    {
        private readonly string _name;
        private readonly string _json;

        public GetExecutableStep(string name, string json)
        {
            _name = name;
            _json = json;
        }

        public DeserializeCommandStep GetCommand(Func<string, Type> getCommand)
        {
            var type = getCommand.Invoke(_name);
            return new DeserializeCommandStep(_json, type);
        }

        public DeserializeQueryStep GetQuery(Func<string, Type> getQuery)
        {
            var type = getQuery.Invoke(_name);
            return new DeserializeQueryStep(_json, type);
        }
    }
}