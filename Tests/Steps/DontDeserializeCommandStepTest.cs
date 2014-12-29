using System;
using NSubstitute;
using NUnit.Framework;
using Qvc.Steps;
using Qvc.Steps.Implementations;
using Shouldly;

namespace Tests.Steps
{
    [TestFixture]
    public class DontDeserializeCommandStepTest
    {
        private IDeserializeCommandStep _step;

        [SetUp]
        public void Setup()
        {
            _step = new DontDeserializeCommandStep(new Exception("blabla"));
        }

        [Test]
        public void Test()
        {
            var spy = Substitute.For<Func<string, Type, object>>();
            _step.DeserializeCommand(spy).ShouldBeOfType<DontFindCommandHandlerStep>();
            spy.DidNotReceive().Invoke(Arg.Any<string>(), Arg.Any<Type>());
        }
    }
}