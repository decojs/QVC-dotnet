using NUnit.Framework;
using Qvc;
using Qvc.Exceptions;
using Qvc.Executables;
using Qvc.Results;
using Qvc.Steps;
using Shouldly;

using Tests.TestMaterial;

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
            var result = CommandSteps.FindCommandHandler(_command, c =>
            {
                c.ShouldBe(_command);
                return typeof(CommandHandlerB);
            });

            result.HandlerType.ShouldBe(typeof(CommandHandlerB));
            result.Command.ShouldBe(_command);
        }

        [Test]
        public void TestHandlerDoesNotExist()
        {
            Should.Throw<CommandHandlerDoesNotExistException>(() =>
                CommandSteps.FindCommandHandler(_command, c =>
                {
                    throw new CommandHandlerDoesNotExistException(c.GetType().FullName);
                }));
        }

        [Test]
        public void TestDuplicateHandler()
        {
            Should.Throw<DuplicateCommandHandlerException>(() =>
                CommandSteps.FindCommandHandler(_command, c =>
                {
                    throw new DuplicateCommandHandlerException(c.GetType().FullName);
                }));
        }
    }
}