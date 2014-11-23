using NUnit.Framework;
using Qvc.Exceptions;
using Qvc.Executables;
using Qvc.Handlers;
using Qvc.Repository;
using Shouldly;

namespace Tests.Repository
{
    [TestFixture]
    public class HandlerRepositoryTest
    {
        private HandlerRepository _repo;

        [SetUp]
        public void Setup()
        {
            _repo = new HandlerRepository();
            _repo.AddCommandHandler(typeof(CommandA), typeof(CommandHandlerA));
            _repo.AddQueryHandler(typeof(QueryA), typeof(QueryHandlerA));
        }

        [Test]
        public void FindCommandHandler()
        {
            _repo.FindCommandHandler(typeof(CommandA)).ShouldBe(typeof(CommandHandlerA));
        }

        [Test]
        public void FindQueryHandler()
        {
            _repo.FindQueryHandler(typeof(QueryA)).ShouldBe(typeof(QueryHandlerA));
        }

        [Test]
        public void FindMissingCommandHandler()
        {
            Should.Throw<CommandHandlerDoesNotExistException>(() => _repo.FindCommandHandler(typeof (CommandB)));
        }

        [Test]
        public void FindMissingQueryHandler()
        {
            Should.Throw<QueryHandlerDoesNotExistException>(() => _repo.FindQueryHandler(typeof(QueryB)));
        }

        [Test]
        public void AddMultipleHandlersToSameCommand()
        {
            Should.Throw<DuplicateCommandHandlerException>(() => _repo.AddCommandHandler(typeof(CommandA), typeof(CommandHandlerA2)));
        }

        [Test]
        public void AddMultipleHandlersToSameQuery()
        {
            Should.Throw<DuplicateQueryHandlerException>(() => _repo.AddQueryHandler(typeof(QueryA), typeof(QueryHandlerA2)));
        }
    }

    class CommandHandlerA : IHandleCommand<CommandA>
    {
        public void Handle(CommandA command) { }
    }

    class QueryHandlerA : IHandleQuery<QueryA, int>
    {
        public int Handle(QueryA command) { return 0; }
    }

    class CommandHandlerA2 : IHandleCommand<CommandA>
    {
        public void Handle(CommandA command) { }
    }

    class QueryHandlerA2 : IHandleQuery<QueryA, int>
    {
        public int Handle(QueryA command) { return 0; }
    }

}