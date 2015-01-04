using System;

namespace Qvc.Steps
{
    public interface IGetQueryStep
    {
        IDeserializeQueryStep GetQuery(Func<string, Type> getQuery);
    }
}