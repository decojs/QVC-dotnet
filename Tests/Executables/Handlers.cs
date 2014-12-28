using Qvc.Handlers;

namespace Tests.Executables
{
    class CommandHandlerA : IHandleCommand<CommandA>
    {
        public void Handle(CommandA command) { }
    }

    class QueryHandlerA : IHandleQuery<QueryA, int>
    {
        public int Handle(QueryA command) { return 0; }
    }

    class CommandHandlerB : IHandleCommand<CommandB>
    {
        public void Handle(CommandB command) { }
    }

    class QueryHandlerB : IHandleQuery<QueryB, int>
    {
        public int Handle(QueryB command) { return 0; }
    }
}