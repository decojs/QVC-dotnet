using NUnit.Framework;
using Qvc;
using Qvc.Exceptions;
using Qvc.Results;
using Qvc.Steps;
using Shouldly;
using Tests.Executables;

namespace Tests.Steps
{
    [TestFixture]
    public class CommandNameAndJsonTest
    {
        private CommandNameAndJson _step;

        [SetUp]
        public void Setup()
        {
            _step = new CommandNameAndJson("name", "json");
        }

        [Test]
        public void TestCommand()
        {
            _step.FindCommand(name =>
            {
                name.ShouldBe("name");
                return typeof(CommandA);
            });
        }

        [Test]
        public void TestCommandDoesNotExist()
        {
            _step.FindCommand(name =>
            {
                throw new CommandDoesNotExistException(name);
            }).ShouldBeOfType<CommandResult>();
        }

        [Test]
        public void TestCommandExecutableDoesNotExist()
        {
            _step.FindCommand(name =>
            {
                throw new ExecutableDoesNotExistException(name);
            }).ShouldBeOfType<CommandResult>();
        }
    }
}