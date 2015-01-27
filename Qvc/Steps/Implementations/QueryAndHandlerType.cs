using System;
using Qvc.Executables;

namespace Qvc.Steps.Implementations
{
    public class QueryAndHandlerType : IQueryAndHandlerType
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