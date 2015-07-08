using System;

namespace Qvc.Exceptions
{
    public class AsyncVoidException : Exception
    {
        public AsyncVoidException(string message) : base(message)
        {
        }
    }
}