using System;
using Newtonsoft.Json;
using NUnit.Framework;
using Qvc.Executables;
using Qvc.Handlers;
using Qvc.Repository;
using Qvc.Steps;

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
            new GetExecutableStep("CommandFullTest", "{}")
                .GetCommand(_repo.FindCommand)
                .DeserializeCommand(JsonConvert.DeserializeObject)
                .FindCommandHandler(_handlerRepo.FindCommandHandler)
                .CreateCommandHandler(Activator.CreateInstance)
                .HandleCommand(Qvc.Default.CommandExecutor)
                .Serialize(JsonConvert.SerializeObject);
        }
        
        [Test]
        public void ExecuteQuery()
        {
            new GetExecutableStep("QueryFullTest", "{}")
                .GetQuery(_repo.FindQuery)
                .DeserializeQuery(JsonConvert.DeserializeObject)
                .FindQueryHandler(_handlerRepo.FindQueryHandler)
                .CreateQueryHandler(Activator.CreateInstance)
                .HandleQuery(Qvc.Default.QueryExecutor)
                .Serialize(JsonConvert.SerializeObject);
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