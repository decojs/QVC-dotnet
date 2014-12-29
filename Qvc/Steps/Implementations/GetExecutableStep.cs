using System;
using Qvc.Exceptions;

namespace Qvc.Steps.Implementations
{
    public class GetExecutableStep : IGetExecutableStep
    {
        private readonly string _name;
        private readonly string _json;

        public GetExecutableStep(string name, string json)
        {
            _name = name;
            _json = json;
        }

        public IDeserializeCommandStep GetCommand(Func<string, Type> getCommand)
        {
            try
            {
                var type = getCommand.Invoke(_name);
                return new DeserializeCommandStep(_json, type);
            }
            catch (CommandDoesNotExistException e)
            {
                return new DontDeserializeCommandStep(e);
            }
            catch (ExecutableDoesNotExistException e)
            {
                return new DontDeserializeCommandStep(e);
            }
        }

        public IDeserializeQueryStep GetQuery(Func<string, Type> getQuery)
        {
            try
            {
                var type = getQuery.Invoke(_name);
                return new DeserializeQueryStep(_json, type);
            }
            catch (QueryDoesNotExistException e)
            {
                return new DontDeserializeQueryStep(e);
            }
            catch (ExecutableDoesNotExistException e)
            {
                return new DontDeserializeQueryStep(e);
            }
        }
    }
}