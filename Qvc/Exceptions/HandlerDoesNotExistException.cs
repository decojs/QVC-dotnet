using System;

namespace Qvc.Exceptions
{
    public class CommandHandlerDoesNotExistException : Exception
    {
        public CommandHandlerDoesNotExistException(string name)
            : base("Could not find a handler for command "+name)
        {

        }
    }

    public class QueryHandlerDoesNotExistException : Exception
    {
        public QueryHandlerDoesNotExistException(string name)
            : base("Could not find a handler for query " + name)
        {

        }
    }
}