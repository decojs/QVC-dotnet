using NUnit.Framework;
using Qvc;
using Qvc.Repository;

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
                .ThenSerialize();
            result.ShouldBe("{\"Success\":true,\"Valid\":true,\"Exception\":null,\"Violations\":[]}");
        }

        [Test]
        public async void ExecuteQuery()
        {
            var result = await Action.Query("QueryFullTest", "{}")
                .Then(q => QuerySteps.FindQuery(q, _repo.FindQuery))
                .Then(QuerySteps.DeserializeQuery)
                .Then(QuerySteps.ValidateQuery)
                .Then(q => QuerySteps.FindQueryHandler(q, _handlerRepo.FindQueryHandler))
                .Then(QuerySteps.CreateQueryHandler)
                .Then(QuerySteps.HandleQuery)
                .Catch(QuerySteps.ExceptionToQueryResult)
                .Then(QuerySteps.Serialize);
            result.ShouldBe("{\"Result\":\"hello\",\"Success\":true,\"Valid\":true,\"Exception\":null,\"Violations\":[]}");
        }

        [Test]
        public async void GetConstraints()
        {
            var result = await Action.Constraints("QueryFullTest")
                .Then(name => ConstraintsSteps.FindExecutable(name, _repo.FindExecutable))
                .Then(ConstraintsSteps.GetConstraints)
                .Then(ConstraintsSteps.Serialize);
            result.ShouldBe("{\"Parameters\":null}");
        }
    }
}