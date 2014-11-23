namespace Qvc.Exceptions
{
    public class ExecutableDoesNotExistException : System.Exception
    {
        public ExecutableDoesNotExistException(string message)
            : base(message + " is not a Command or a Query")
        {

        }
    }
    public class CommandDoesNotExistException : System.Exception
    {
        public CommandDoesNotExistException(string message)
            : base(message + " is not a Command")
        {

        }
    }
    public class QueryDoesNotExistException : System.Exception
    {
        public QueryDoesNotExistException(string message)
            : base(message + " is not a Query")
        {

        }
    }
}