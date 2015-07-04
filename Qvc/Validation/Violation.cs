namespace Qvc.Validation
{
    public class Violation
    {
        public Violation(string fieldName, string message)
        {
            FieldName = fieldName;
            Message = message;
        }

        public string FieldName { get; private set; }
        
        public string Message { get; private set; }
    }
}