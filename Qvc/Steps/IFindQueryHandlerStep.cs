using System;
using Qvc.Executables;

namespace Qvc.Steps
{
    public interface IFindQueryHandlerStep
    {
        ICreateQueryHandlerStep FindQueryHandler(Func<IQuery, Type> findQueryHandler);
    }
}