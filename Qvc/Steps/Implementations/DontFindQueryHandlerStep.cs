using System;
using Qvc.Executables;

namespace Qvc.Steps.Implementations
{
    public class DontFindQueryHandlerStep : IFindQueryHandlerStep
    {
        private readonly Exception _exception;

        public DontFindQueryHandlerStep(Exception exception)
        {
            _exception = exception;
        }

        public ICreateQueryHandlerStep FindQueryHandler(Func<IQuery, Type> findQueryHandler)
        {
            return new DontCreateQueryHandlerStep(_exception);
        }
    }
}