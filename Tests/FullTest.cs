using NUnit.Framework;
using Qvc;
using Qvc.Executables;
using Qvc.Handlers;
using Qvc.Repository;

namespace Tests
{
    [TestFixture]
    public class FullTest
    {
        private ExecutableRepository _repo;
        private HandlerRepository _handlerRepo;

        [SetUp]
        public void Setup()
        {
            _repo = new ExecutableRepository();
            _handlerRepo = new HandlerRepository();
            Qvc.Reflection.Setup.SetupRepositories(_handlerRepo, _repo);
        }

        [Test]
        public void ExecuteCommand()
        {
            Action.Command("CommandFullTest", "{}")
                .GetCommand(_repo.FindCommand)
                .DeserializeCommand()
                .FindCommandHandler(_handlerRepo.FindCommandHandler)
                .CreateCommandHandler()
                .HandleCommand()
                .Serialize();
        }
        
        [Test]
        public void ExecuteQuery()
        {
            Action.Query("QueryFullTest", "{}")
                .GetQuery(_repo.FindQuery)
                .DeserializeQuery()
                .FindQueryHandler(_handlerRepo.FindQueryHandler)
                .CreateQueryHandler()
                .HandleQuery()
                .Serialize();
        }
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

    class CommandFullTest : ICommand { }

    class QueryFullTest : IQuery { }
}