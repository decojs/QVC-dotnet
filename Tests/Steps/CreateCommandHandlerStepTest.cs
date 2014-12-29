using System;
using NUnit.Framework;
using Qvc.Steps;
using Qvc.Steps.Implementations;
using Shouldly;
using Tests.Executables;

namespace Tests.Steps
{
    [TestFixture]
    public class CreateCommandHandlerStepTest
    {
        private ICreateCommandHandlerStep _step;

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
            }).ShouldBeOfType<ExecuteCommandStep>();
        }

        [Test]
        public void TestThrowsException()
        {
            _step.CreateCommandHandler(h =>
            {
                throw new Exception("could not be made");
            }).ShouldBeOfType<DontExecuteCommandStep>();
        }
    }
}