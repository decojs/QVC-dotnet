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

        public static Type GetCommandHandledByHandler(Type handler)
        {
            return handler.GetInterface(typeof(IHandleCommand<>).FullName).GenericTypeArguments.First();
        }

        public static Type GetQueryHandledByHandler(Type handler)
        {
            return handler.GetInterface(typeof(IHandleQuery<,>).FullName).GenericTypeArguments.First();
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
    }
}