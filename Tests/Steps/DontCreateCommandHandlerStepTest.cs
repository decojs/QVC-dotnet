using System;
using NSubstitute;
using NUnit.Framework;
using Qvc.Steps;
using Qvc.Steps.Implementations;
using Shouldly;

namespace Tests.Steps
{
    [TestFixture]
    public class DontCreateCommandHandlerStepTest
    {
        private ICreateCommandHandlerStep _step;

        [SetUp]
        public void Setup()
        {
            _step = new DontCreateCommandHandlerStep(new Exception());
        }

        [Test]
        public void Test()
        {
            var spy = Substitute.For<Func<Type, object>>();
            _step.CreateCommandHandler(spy).ShouldBeOfType<DontExecuteCommandStep>();
            spy.DidNotReceive().Invoke(Arg.Any<Type>());
        }
    }
}