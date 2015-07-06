using System;
using System.Collections.Generic;
using System.Linq;
using Qvc.Handlers;

namespace Qvc.Reflection
{
    public class Reflection
    {
        public static IEnumerable<Type> FindCommandHandlers()
        {
            return GetImplementationsOf(typeof(IHandleCommand<>));
        }
        
        public static IEnumerable<Type> FindQueryHandlers()
        {
            return GetImplementationsOf(typeof(IHandleQuery<,>));
        }

        public static IEnumerable<Type> GetCommandsHandledByHandler(Type handler)
        {
            return GetExecutablesHandledByHandler(handler, typeof(IHandleCommand<>));
        }

        public static IEnumerable<Type> GetQueriesHandledByHandler(Type handler)
        {
            return GetExecutablesHandledByHandler(handler, typeof(IHandleQuery<,>));
        }

        public static IEnumerable<Type> GetImplementationsOf(Type type)
        {
            return AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(found => !found.IsAbstract && !found.IsInterface)
                .Where(found => found.GetInterfaces()
                    .Where(i => i.IsGenericType)
                    .Select(i => i.GetGenericTypeDefinition())
                    .Contains(type));
        }

        private static IEnumerable<Type> GetExecutablesHandledByHandler(Type handler, Type executableType)
        {
            return
                handler.GetInterfaces()
                    .Where(iface => iface.IsGenericType && iface.GetGenericTypeDefinition() == executableType)
                    .Select(iface => iface.GenericTypeArguments.First());
        }
    }
}