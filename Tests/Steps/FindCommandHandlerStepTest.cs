using NUnit.Framework;
using Qvc.Exceptions;
using Qvc.Executables;
using Qvc.Steps;
using Qvc.Steps.Implementations;
using Shouldly;
using Tests.Executables;

namespace Tests.Steps
{
    [TestFixture]
    public class FindCommandHandlerStepTest
    {
        private IFindCommandHandlerStep _step;
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
            }).ShouldBeOfType<CreateCommandHandlerStep>();
        }

        [Test]
        public void TestHandlerDoesNotExist()
        {
            _step.FindCommandHandler(c =>
            {
                throw new CommandHandlerDoesNotExistException(c.GetType().FullName);
            }).ShouldBeOfType<ErrorStep>();
        }

        [Test]
        public void TestDuplicateHandler()
        {
            _step.FindCommandHandler(c =>
            {
                throw new DuplicateCommandHandlerException(c.GetType().FullName);
            }).ShouldBeOfType<ErrorStep>();
        }
    }
}