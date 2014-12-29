using System;

namespace Qvc.Steps
{
    public interface IDeserializeQueryStep
    {
        IFindQueryHandlerStep DeserializeQuery(Func<string, Type, object> deserializeTheQuery);

        IFindQueryHandlerStep DeserializeQuery();
    }
}