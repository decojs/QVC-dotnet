using System;
using NSubstitute;
using NUnit.Framework;
using Qvc.Steps;
using Qvc.Steps.Implementations;
using Shouldly;

namespace Tests.Steps
{
    [TestFixture]
    public class DontDeserializeQueryStepTest
    {
        private IDeserializeQueryStep _step;

        [SetUp]
        public void Setup()
        {
            _step = new DontDeserializeQueryStep(new Exception("blabla"));
        }

        [Test]
        public void Test()
        {
            var spy = Substitute.For<Func<string, Type, object>>();
            _step.DeserializeQuery(spy).ShouldBeOfType<DontFindQueryHandlerStep>();
            spy.DidNotReceive().Invoke(Arg.Any<string>(), Arg.Any<Type>());
        }
    }
}