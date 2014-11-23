using System.Linq;
using Qvc.Repository;

namespace Qvc.Reflection
{
    public class Setup
    {
        public static void SetupRepositories(HandlerRepository handlerRepository, ExecutableRepository executableRepository)
        {
            AddCommandsAndHandlers(handlerRepository, executableRepository);

            AddQueriesAndHandlers(handlerRepository, executableRepository);
        }

        private static void AddQueriesAndHandlers(HandlerRepository handlerRepository, ExecutableRepository executableRepository)
        {
            var queryHandlers = Reflection.FindQueryHandlers()
                .Select(h => new {Handler = h, Command = Reflection.GetQueryHandledByHandler(h)})
                .ToList();

            queryHandlers.ForEach(c => handlerRepository.AddQueryHandler(c.Command, c.Handler));
            executableRepository.AddExecutables(queryHandlers.Select(c => c.Command));
        }

        private static void AddCommandsAndHandlers(HandlerRepository handlerRepository, ExecutableRepository executableRepository)
        {
            var commandHandlers = Reflection.FindCommandHandlers()
                .Select(h => new {Handler = h, Command = Reflection.GetCommandHandledByHandler(h)})
                .ToList();

            commandHandlers.ForEach(c => handlerRepository.AddCommandHandler(c.Command, c.Handler));
            executableRepository.AddExecutables(commandHandlers.Select(c => c.Command));
        }
    }
}