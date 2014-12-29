using System;
using NSubstitute;
using NUnit.Framework;
using Qvc.Steps;
using Qvc.Steps.Implementations;
using Shouldly;

namespace Tests.Steps
{
    [TestFixture]
    public class DontCreateQueryHandlerStepTest
    {
        private ICreateQueryHandlerStep _step;

        [SetUp]
        public void Setup()
        {
            _step = new DontCreateQueryHandlerStep(new Exception("blabla"));
        }

        [Test]
        public void Test()
        {
            var spy = Substitute.For<Func<Type, object>>();
            _step.CreateQueryHandler(spy).ShouldBeOfType<DontExecuteQueryStep>();
            spy.DidNotReceive().Invoke(Arg.Any<Type>());
        }
    }
}