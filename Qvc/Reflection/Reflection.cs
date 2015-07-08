using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Qvc.Handlers;

namespace Qvc.Reflection
{
    public class Reflection
    {
        public static IReadOnlyCollection<Type> FindCommandHandlers(IReadOnlyCollection<Type> types)
        {
            return GetImplementationsOfGenericInterface(typeof(IHandleCommand<>), types)
                .Concat(GetImplementationsOfGenericInterface(typeof(IHandleCommandAsync<>), types))
                .Distinct()
                .ToList();
        }
        
        public static IReadOnlyCollection<Type> FindQueryHandlers(IReadOnlyCollection<Type> types)
        {
            return GetImplementationsOfGenericInterface(typeof(IHandleQuery<,>), types)
                .Concat(GetImplementationsOfGenericInterface(typeof(IHandleQueryAsync<,>), types))
                .Distinct()
                .ToList();
        }

        public static IReadOnlyCollection<Type> GetCommandsHandledByHandler(Type handler)
        {
            return GetFirstGenericArgumentFromInterfacesOfType(handler, typeof(IHandleCommand<>))
                .Concat(GetFirstGenericArgumentFromInterfacesOfType(handler, typeof(IHandleCommandAsync<>)))
                .Distinct()
                .ToList();
        }

        public static IReadOnlyCollection<Type> GetQueriesHandledByHandler(Type handler)
        {
            return GetFirstGenericArgumentFromInterfacesOfType(handler, typeof(IHandleQuery<,>))
                .Concat(GetFirstGenericArgumentFromInterfacesOfType(handler, typeof(IHandleQueryAsync<,>)))
                .Distinct()
                .ToList();
        }

        public static IReadOnlyCollection<Type> GetImplementationsOfGenericInterface(Type genericInterfaceType, IEnumerable<Type> types)
        {
            return types
                .Where(found => !found.IsAbstract && !found.IsInterface)
                .Where(found => found.GetInterfaces()
                    .Where(i => i.IsGenericType)
                    .Select(i => i.GetGenericTypeDefinition())
                    .Contains(genericInterfaceType))
                .ToList();
        }

        public static IReadOnlyCollection<Type> GetFirstGenericArgumentFromInterfacesOfType(Type type, Type genericInterfaceType)
        {
            return
                type.GetInterfaces()
                    .Where(iface => iface.IsGenericType && iface.GetGenericTypeDefinition() == genericInterfaceType)
                    .Select(iface => iface.GenericTypeArguments.First())
                    .ToList();
        }

        public static IReadOnlyCollection<Type> FindAllTypes()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .ToList();
        }

        public static MethodInfo GetHandleMethod(Type executableType, Type handlerType)
        {
            return handlerType.GetMethod("Handle", new[] { executableType });
        }
    }
}