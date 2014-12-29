using System;
using Qvc.Executables;
using Qvc.Handlers;

namespace Qvc.Steps
{
    public interface IExecuteQueryStep
    {
        ISerializeResultStep HandleQuery(Func<IHandleExecutable, IQuery, object> executeQuery);

        ISerializeResultStep HandleQuery();
    }
}