namespace Qvc.Results
{
    public class CommandResult : ExecutableResult
    {
        public CommandResult()
        {
            Success = true;
            Valid = true;
            Exception = null;
        }
    }
}