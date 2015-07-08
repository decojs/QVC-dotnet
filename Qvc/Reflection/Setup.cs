using System;
using System.Collections.Generic;
using System.Linq;
using Qvc.Repository;

namespace Qvc.Reflection
{
    public class Setup
    {
        public static void SetupRepositories(
            HandlerRepository handlerRepository, 
            ExecutableRepository executableRepository)
        {
            SetupRepositories(handlerRepository, executableRepository, Reflection.FindAllTypes());
        }

        public static void SetupRepositories(
            HandlerRepository handlerRepository, 
            ExecutableRepository executableRepository, 
            IReadOnlyCollection<Type> types)
        {
            AddCommandsAndHandlers(handlerRepository, executableRepository, types);

            AddQueriesAndHandlers(handlerRepository, executableRepository, types);
        }

        private static void AddQueriesAndHandlers(
            HandlerRepository handlerRepository, 
            ExecutableRepository executableRepository, 
            IEnumerable<Type> types)
        {
            var queryHandlers = Reflection.FindQueryHandlers(types)
                    .SelectMany(h => Reflection.GetQueriesHandledByHandler(h).Select(c => new { Handler = h, Query = c }))
                    .ToList();

            queryHandlers.ForEach(c => handlerRepository.AddQueryHandler(c.Query, c.Handler));
            executableRepository.AddExecutables(queryHandlers.Select(c => c.Query));
        }

        private static void AddCommandsAndHandlers(
            HandlerRepository handlerRepository, 
            ExecutableRepository executableRepository, 
            IEnumerable<Type> types)
        {
            var commandHandlers = Reflection.FindCommandHandlers(types)
                .SelectMany(h => Reflection.GetCommandsHandledByHandler(h).Select(c => new { Handler = h, Command = c }))
                .ToList();

            commandHandlers.ForEach(c => handlerRepository.AddCommandHandler(c.Command, c.Handler));
            executableRepository.AddExecutables(commandHandlers.Select(c => c.Command));
        }
    }
}