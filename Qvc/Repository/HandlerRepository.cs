using System;
using System.Collections.Generic;
using Qvc.Exceptions;
using Qvc.Executables;

namespace Qvc.Repository
{
    public class HandlerRepository
    {
        private readonly IDictionary<Type, Type> _commandHandlers = new Dictionary<Type, Type>();
        private readonly IDictionary<Type, Type> _queryHandlers = new Dictionary<Type, Type>();

        public void AddCommandHandler(Type commandType, Type commandHandlerType)
        {
            if (_commandHandlers.ContainsKey(commandType))
            {
                throw new DuplicateCommandHandlerException(commandType.FullName);
            }

            _commandHandlers[commandType] = commandHandlerType;
        }

        public void AddQueryHandler(Type queryType, Type queryHandlerType)
        {
            if (_queryHandlers.ContainsKey(queryType))
            {
                throw new DuplicateQueryHandlerException(queryType.FullName);
            }
            _queryHandlers[queryType] = queryHandlerType;
        }

        public Type FindCommandHandler(Type commandType)
        {
            if (!_commandHandlers.ContainsKey(commandType))
            {
                throw new CommandHandlerDoesNotExistException(commandType.FullName);
            }

            return _commandHandlers[commandType];
        }

        public Type FindQueryHandler(Type queryType)
        {
            if (!_queryHandlers.ContainsKey(queryType))
            {
                throw new QueryHandlerDoesNotExistException(queryType.FullName);
            }

            return _queryHandlers[queryType];
        }

        public Type FindCommandHandler(ICommand command)
        {
            var commandType = command.GetType();

            if (!_commandHandlers.ContainsKey(commandType))
            {
                throw new CommandHandlerDoesNotExistException(commandType.FullName);
            }

            return _commandHandlers[commandType];
        }

        public Type FindQueryHandler(IQuery query)
        {
            var queryType = query.GetType();

            if (!_queryHandlers.ContainsKey(queryType))
            {
                throw new QueryHandlerDoesNotExistException(queryType.FullName);
            }

            return _queryHandlers[queryType];
        }
    }
}