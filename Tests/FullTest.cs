using System.Diagnostics;
using NUnit.Framework;
using Qvc;
using Qvc.Executables;
using Qvc.Handlers;
using Qvc.Repository;
using Qvc.Results;
using Shouldly;

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
                .ThenFindCommand(_repo.FindCommand)
                .ThenDeserializeCommand()
                .ThenValidateCommand()
                .ThenFindCommandHandler(_handlerRepo.FindCommandHandler)
                .ThenCreateCommandHandler()
                .ThenHandleCommand()
                .ThenSerialize()
                .Done().ShouldBe("{\"Success\":true,\"Valid\":true,\"Exception\":null,\"Violations\":[]}");
        }
        
        [Test]
        public void ExecuteQuery()
        {
            Action.Query("QueryFullTest", "{}")
                .Then(q => QuerySteps.FindQuery(q, _repo.FindQuery))
                .Then(q => QuerySteps.DeserializeQuery(q))
                .Then(q => QuerySteps.ValidateQuery(q))
                .Then(q => QuerySteps.FindQueryHandler(q, _handlerRepo.FindQueryHandler))
                .Then(q => QuerySteps.CreateQueryHandler(q))
                .Then(q => QuerySteps.HandleQuery(q))
                .Catch(e => new QueryResult(e))
                .Then(q => QuerySteps.Serialize(q))
                .Done().ShouldBe("{\"Result\":\"hello\",\"Success\":true,\"Valid\":true,\"Exception\":null,\"Violations\":[]}");
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