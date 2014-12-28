using NUnit.Framework;
using Qvc.Repository;
using Shouldly;
using Tests.Executables;
using Tests.Repository;

namespace Tests.Reflection
{
    [TestFixture]
    public class SetupTest
    {
        private ExecutableRepository _executableRepo;
        private HandlerRepository _handlerRepo;

        [SetUp]
        public void Setup()
        {
            _executableRepo = new ExecutableRepository();
            _handlerRepo = new HandlerRepository();
            Qvc.Reflection.Setup.SetupRepositories(_handlerRepo, _executableRepo);
        }

        [Test]
        public void SetupRepos()
        {
            _executableRepo.FindCommand("CommandA").ShouldBe(typeof(CommandA));
            _handlerRepo.FindCommandHandler(_executableRepo.FindCommand("CommandB")).ShouldBe(typeof(CommandHandlerB));

            _executableRepo.FindQuery("QueryA").ShouldBe(typeof(QueryA));
            _handlerRepo.FindQueryHandler(_executableRepo.FindQuery("QueryB")).ShouldBe(typeof(QueryHandlerB));
        }
    }
}