using System;
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
        public void Handle(CommandB command) { throw new NullReferenceException(); }
    }

    class QueryHandlerB : IHandleQuery<QueryB, int>
    {
        public int Handle(QueryB command) { throw new NullReferenceException(); }
    }

    class FullTestHandler :
        IHandleCommand<CommandFullTest>,
        IHandleQuery<QueryFullTest, string>
    {
        public void Handle(CommandFullTest command)
        {

        }

        public string Handle(QueryFullTest query)
        {
            return "hello";
        }
    }
}