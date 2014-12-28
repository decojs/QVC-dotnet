namespace Qvc.Results
{
    public class QueryResult : ExecutableResult
    {
        public object Result { get; private set; }

        public QueryResult(object result)
        {
            Success = true;
            Valid = true;
            Exception = null;
            Result = result;
        }
    }
}