using System;
using Qvc.Executables;

namespace Qvc.Steps
{
    public class FindQueryHandlerStep
    {
        private readonly IQuery _query;

        public FindQueryHandlerStep(IQuery query)
        {
            _query = query;
        }

        public CreateQueryHandlerStep FindQueryHandler(Func<IQuery, Type> findQueryHandler)
        {
            var handlerType = findQueryHandler.Invoke(_query);
            return new CreateQueryHandlerStep(_query, handlerType);
        }
    }
}