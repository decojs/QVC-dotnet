using System;
using Qvc.Exceptions;
using Qvc.Executables;

namespace Qvc.Steps.Implementations
{
    public class FindQueryHandlerStep : IFindQueryHandlerStep
    {
        public IQuery Query { get; private set; }

        public FindQueryHandlerStep(IQuery query)
        {
            Query = query;
        }

        public ICreateQueryHandlerStep FindQueryHandler(Func<IQuery, Type> findQueryHandler)
        {
            try
            {
                var handlerType = findQueryHandler.Invoke(Query);
                return new CreateQueryHandlerStep(Query, handlerType);
            }
            catch (QueryHandlerDoesNotExistException e)
            {
                return new QueryErrorStep(e);
            }
            catch (DuplicateQueryHandlerException e)
            {
                return new QueryErrorStep(e);
            }
        }
    }
}