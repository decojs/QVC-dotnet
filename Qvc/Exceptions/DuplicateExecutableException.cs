using System;

namespace Qvc.Exceptions
{
    public class DuplicateExecutableException : Exception
    {
        public DuplicateExecutableException(string message) : base(message)
        {
            
        }
    }
}