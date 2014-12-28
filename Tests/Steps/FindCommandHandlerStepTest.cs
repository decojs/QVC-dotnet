using NUnit.Framework;
using Qvc.Executables;
using Qvc.Steps;
using Shouldly;
using Tests.Executables;
using Tests.Repository;

namespace Tests.Steps
{
    [TestFixture]
    public class FindCommandHandlerStepTest
    {
        private FindCommandHandlerStep _step;
        private ICommand _command;

        [SetUp]
        public void Setup()
        {
            _command = new CommandB();
            _step = new FindCommandHandlerStep(_command);
        }

        [Test]
        public void Test()
        {
            _step.FindCommandHandler(c =>
            {
                c.ShouldBe(_command);
                return typeof(CommandHandlerB);
            });
        }
    }
}