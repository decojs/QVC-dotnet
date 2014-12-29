using System;
using Qvc.Executables;

namespace Qvc.Steps.Implementations
{
    public class FindQueryHandlerStep : IFindQueryHandlerStep
    {
        private readonly IQuery _query;

        public FindQueryHandlerStep(IQuery query)
        {
            _query = query;
        }

        public ICreateQueryHandlerStep FindQueryHandler(Func<IQuery, Type> findQueryHandler)
        {
            var handlerType = findQueryHandler.Invoke(_query);
            return new CreateQueryHandlerStep(_query, handlerType);
        }
    }
}