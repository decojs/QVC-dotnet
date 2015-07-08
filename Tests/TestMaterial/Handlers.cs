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

    class AsyncVoidHandler : IHandleCommand<AsyncVoidCommand>
    {
        public async void Handle(AsyncVoidCommand command)
        {
        }
    }

    class FullTestHandler :
        IHandleCommandAsync<CommandFullTest>,
        IHandleQuery<QueryFullTest, string>,
        IHandleCommandAsync<CommandThatThrows>,
        IHandleQueryAsync<QueryThatThrows, string>,
        IHandleCommandAsync<CommandThatThrowsValidationException>,
        IHandleQueryAsync<QueryThatThrowsValidationException, string>
    {
        public async Task Handle(CommandFullTest command)
        {
        }

        public string Handle(QueryFullTest query)
        {
            return "hello";
        }

        public Task Handle(CommandThatThrows command)
        {
            throw new NotImplementedException();
        }

        public Task<string> Handle(QueryThatThrows query)
        {
            throw new NotImplementedException();
        }

        public Task Handle(CommandThatThrowsValidationException command)
        {
            throw new ValidationException("oops");
        }

        public Task<string> Handle(QueryThatThrowsValidationException query)
        {
            throw new ValidationException("oops");
        }
    }
}