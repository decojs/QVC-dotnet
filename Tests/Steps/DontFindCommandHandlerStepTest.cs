using System;
using NSubstitute;
using NUnit.Framework;
using Qvc.Executables;
using Qvc.Steps;
using Qvc.Steps.Implementations;
using Shouldly;

namespace Tests.Steps
{
    [TestFixture]
    public class DontFindCommandHandlerStepTest
    {
        private IFindCommandHandlerStep _step;

        [SetUp]
        public void Setup()
        {
            _step = new DontFindCommandHandlerStep(new Exception("blabla"));
        }

        [Test]
        public void Test()
        {
            var spy = Substitute.For<Func<ICommand, Type>>();
            _step.FindCommandHandler(spy).ShouldBeOfType<DontCreateCommandHandlerStep>();
            spy.DidNotReceive().Invoke(Arg.Any<ICommand>());
        }
    }
}