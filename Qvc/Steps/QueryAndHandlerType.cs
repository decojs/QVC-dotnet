using System;
using Qvc.Executables;

namespace Qvc.Steps
{
    public class QueryAndHandlerType
    {
        public IQuery Query { get; private set; }

        public Type HandlerType { get; private set; }

        public QueryAndHandlerType(IQuery query, Type handlerType)
        {
            Query = query;
            HandlerType = handlerType;
        }
    }
}