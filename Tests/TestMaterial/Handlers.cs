using System;
using System.Threading.Tasks;

using Qvc.Handlers;
using Qvc.Validation;

namespace Tests.TestMaterial
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
        IHandleCommandAsync<CommandFullTest>,
        IHandleQuery<QueryFullTest, string>
    {
        public async Task Handle(CommandFullTest command)
        {
        }

        public string Handle(QueryFullTest query)
        {
            return "hello";
        }
    }
}