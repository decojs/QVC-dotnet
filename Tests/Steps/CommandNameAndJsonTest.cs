using NUnit.Framework;
using Qvc;
using Qvc.Exceptions;
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
            var result = CommandSteps.FindCommand(_step, name =>
            {
                name.ShouldBe("name");
                return typeof(CommandA);
            });

            result.Json.ShouldBe("json");
            result.Type.ShouldBe(typeof(CommandA));
        }

        [Test]
        public void TestCommandDoesNotExist()
        {
            Should.Throw<CommandDoesNotExistException>(() =>
                CommandSteps.FindCommand(_step, name =>
                {
                    throw new CommandDoesNotExistException(name);
                }));
        }

        [Test]
        public void TestCommandExecutableDoesNotExist()
        {
            Should.Throw<ExecutableDoesNotExistException>(() =>
                CommandSteps.FindCommand(_step, name =>
                {
                    throw new ExecutableDoesNotExistException(name);
                }));
        }
    }
}