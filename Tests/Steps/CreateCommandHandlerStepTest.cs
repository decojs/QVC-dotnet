using System;
using NUnit.Framework;
using Qvc;
using Qvc.Steps;
using Shouldly;
using Tests.Executables;

namespace Tests.Steps
{
    [TestFixture]
    public class CreateCommandHandlerStepTest
    {
        private CommandAndHandlerType _step;

        private CommandB _command;

        [SetUp]
        public void Setup()
        {
            _command = new CommandB();
            _step = new CommandAndHandlerType(_command, typeof(CommandHandlerB));
        }

        [Test]
        public void Test()
        {
            var result = CommandSteps.CreateCommandHandler(_step, h =>
            {
                h.ShouldBe(typeof(CommandHandlerB));
                return new CommandHandlerB();
            });

            result.Handler.ShouldBeOfType<CommandHandlerB>();
            result.Command.ShouldBe(_command);
        }

        [Test]
        public void TestThrowsException()
        {
            Should.Throw<Exception>(() =>
                CommandSteps.CreateCommandHandler(_step, h =>
                {
                    throw new Exception("could not be made");
                }));
        }
    }
}