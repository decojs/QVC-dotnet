using Qvc.Executables;
using Qvc.Handlers;

namespace Qvc
{
    public class Default
    {
        public static void CommandExecutor(IHandleExecutable handler, ICommand command)
        {
            handler.GetType().GetMethod("Handle", new[] { command.GetType() }).Invoke(handler, new object[] { command });
        }

        public static object QueryExecutor(IHandleExecutable handler, IQuery query)
        {
            return handler.GetType().GetMethod("Handle", new[] { query.GetType() }).Invoke(handler, new object[] { query });
        }
    }
}