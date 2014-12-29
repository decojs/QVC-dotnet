using System;

namespace Qvc.Steps
{
    public interface ICreateQueryHandlerStep
    {
        IExecuteQueryStep CreateQueryHandler(Func<Type, object> createQueryHandler);

        IExecuteQueryStep CreateQueryHandler();
    }
}