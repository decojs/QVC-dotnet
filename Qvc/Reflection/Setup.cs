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
                    .SelectMany(h => Reflection.GetQueriesHandledByHandler(h).Select(c => new { Handler = h, Query = c }))
                    .ToList();

            queryHandlers.ForEach(c => handlerRepository.AddQueryHandler(c.Query, c.Handler));
            executableRepository.AddExecutables(queryHandlers.Select(c => c.Query));
        }

        private static void AddCommandsAndHandlers(HandlerRepository handlerRepository, ExecutableRepository executableRepository)
        {
            var commandHandlers = Reflection.FindCommandHandlers()
                .SelectMany(h => Reflection.GetCommandsHandledByHandler(h).Select(c => new { Handler = h, Command = c }))
                .ToList();

            commandHandlers.ForEach(c => handlerRepository.AddCommandHandler(c.Command, c.Handler));
            executableRepository.AddExecutables(commandHandlers.Select(c => c.Command));
        }
    }
}