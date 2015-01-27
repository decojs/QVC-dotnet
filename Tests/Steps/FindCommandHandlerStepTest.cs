using NUnit.Framework;
using Qvc;
using Qvc.Exceptions;
using Qvc.Executables;
using Qvc.Steps.Implementations;
using Shouldly;
using Tests.Executables;

namespace Tests.Steps
{
    [TestFixture]
    public class FindCommandHandlerStepTest
    {
        private ICommand _command;

        [SetUp]
        public void Setup()
        {
            _command = new CommandB();
        }

        [Test]
        public void Test()
        {
            _command.FindCommandHandler(c =>
            {
                c.ShouldBe(_command);
                return typeof(CommandHandlerB);
            }).ShouldBeOfType<CommandAndHandlerType>();
        }

        [Test]
        public void TestHandlerDoesNotExist()
        {
            _command.FindCommandHandler(c =>
            {
                throw new CommandHandlerDoesNotExistException(c.GetType().FullName);
            }).ShouldBeOfType<CommandErrorStep>();
        }

        [Test]
        public void TestDuplicateHandler()
        {
            _command.FindCommandHandler(c =>
            {
                throw new DuplicateCommandHandlerException(c.GetType().FullName);
            }).ShouldBeOfType<CommandErrorStep>();
        }
    }
}