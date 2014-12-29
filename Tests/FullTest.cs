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
                .DeserializeCommand(Qvc.Default.Deserialize)
                .FindCommandHandler(_handlerRepo.FindCommandHandler)
                .CreateCommandHandler(Qvc.Default.CreateHandler)
                .HandleCommand(Qvc.Default.HandleCommand)
                .Serialize(Qvc.Default.Serialize);
        }
        
        [Test]
        public void ExecuteQuery()
        {
            new GetExecutableStep("QueryFullTest", "{}")
                .GetQuery(_repo.FindQuery)
                .DeserializeQuery(Qvc.Default.Deserialize)
                .FindQueryHandler(_handlerRepo.FindQueryHandler)
                .CreateQueryHandler(Qvc.Default.CreateHandler)
                .HandleQuery(Qvc.Default.HandleQuery)
                .Serialize(Qvc.Default.Serialize);
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