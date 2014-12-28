using NUnit.Framework;
using Qvc.Steps;
using Shouldly;
using Tests.Executables;
using Tests.Repository;

namespace Tests.Steps
{
    [TestFixture]
    public class CreateCommandHandlerStepTest
    {
        private CreateCommandHandlerStep _step;

        [SetUp]
        public void Setup()
        {
            _step = new CreateCommandHandlerStep(new CommandB(), typeof(CommandHandlerB));
        }

        [Test]
        public void Test()
        {
            _step.CreateCommandHandler(h =>
            {
                h.ShouldBe(typeof(CommandHandlerB));
                return new CommandHandlerB();
            });
        }
    }
}