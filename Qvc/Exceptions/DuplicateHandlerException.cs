using System;

namespace Qvc.Exceptions
{
    public class DuplicateCommandHandlerException : Exception
    {
        public DuplicateCommandHandlerException(string name)
            : base("Multiple handlers found for command " + name)
        {

        }
    }

    public class DuplicateQueryHandlerException : Exception
    {
        public DuplicateQueryHandlerException(string name)
            : base("Multiple handlers found for query " + name)
        {

        }
    }
}