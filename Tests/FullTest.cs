using System.Collections.Generic;

using NUnit.Framework;
using Qvc;
using Qvc.Constraints;
using Qvc.Repository;
using Qvc.Results;
using Qvc.Steps;

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
        public async void ExecuteCommand()
        {
            var result = await Action.Command("CommandFullTest", "{}")
                .ThenFindCommand(_repo.FindCommand)
                .ThenDeserializeCommand()
                .ThenValidateCommand()
                .ThenFindCommandHandler(_handlerRepo.FindCommandHandler)
                .ThenCreateCommandHandler()
                .ThenHandleCommand()
                .ThenSerializeResult();
            result.ShouldBe("{\"Success\":true,\"Valid\":true,\"Exception\":null,\"Violations\":[]}");
        }

        [Test]
        public async void ExecuteQuery()
        {
            var result = await Action.Query("QueryFullTest", "{}")
                .ThenFindQuery( _repo.FindQuery)
                .ThenDeserializeQuery()
                .ThenValidateQuery()
                .ThenFindQueryHandler(_handlerRepo.FindQueryHandler)
                .ThenCreateQueryHandler()
                .ThenHandleQuery()
                .ThenSerializeResult();
            result.ShouldBe("{\"Result\":\"hello\",\"Success\":true,\"Valid\":true,\"Exception\":null,\"Violations\":[]}");
        }

        [Test]
        public async void GetConstraints()
        {
            var result = await Action.Constraints("QueryFullTest")
                .ThenFindExecutable(_repo.FindExecutable)
                .ThenGetConstraints(type => new ConstraintsResult(new List<Parameter>()))
                .ThenSerialize();
            result.ShouldBe("{\"Parameters\":[]}");
        }
    }
}