namespace Qvc.Exceptions
{
    public class ExecutableDoesNotExistException : System.Exception
    {
        public ExecutableDoesNotExistException(string message)
            : base(message + " is not a Command or a Query")
        {
            
        }
    }
}