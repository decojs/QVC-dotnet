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

        [SetUp]
        public void Setup()
        {
            _step = new CommandAndHandlerType(new CommandB(), typeof(CommandHandlerB));
        }

        [Test]
        public void Test()
        {
            CommandSteps.CreateCommandHandler(_step, h =>
            {
                h.ShouldBe(typeof(CommandHandlerB));
                return new CommandHandlerB();
            }).ShouldBeOfType<CommandAndHandler>();
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