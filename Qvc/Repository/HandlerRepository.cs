using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

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
            // Command handlers cannot be async void, as we can't catch exceptions from them!
            var handlerMethodInfo = Reflection.Reflection.GetHandleMethod(commandType, commandHandlerType);
            if (MethodIsAsyncVoid(handlerMethodInfo))
            {
                throw new AsyncVoidException(GetAsyncVoidFriendlyExceptionMessage(handlerMethodInfo, commandHandlerType));
            }

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

            return FindCommandHandler(commandType);
        }

        public Type FindQueryHandler(IQuery query)
        {
            var queryType = query.GetType();

            return FindQueryHandler(queryType);
        }

        private static string GetAsyncVoidFriendlyExceptionMessage(MethodBase handlerMethodInfo, Type commandHandlerType)
        {
            var firstParameter = handlerMethodInfo.GetParameters().First();
            var actual = string.Format("async void {0}.{1}({2} {3});", commandHandlerType.FullName, handlerMethodInfo.Name, firstParameter.ParameterType.Name, firstParameter.Name);
            var expected = string.Format("async Task {0}.{1}({2} {3});", commandHandlerType.FullName, handlerMethodInfo.Name, firstParameter.ParameterType.Name, firstParameter.Name);
            return "async method must return a Task!\n" + actual + "\nShould be\n" + expected + "\nUse IHandleCommandAsync instead of IHandleCommand";
        }

        private static bool MethodIsAsyncVoid(MethodInfo handlerMethodInfo)
        {
            return handlerMethodInfo.ReturnType == typeof(void)
            && handlerMethodInfo.GetCustomAttributes<AsyncStateMachineAttribute>().Any();
        }
    }
}