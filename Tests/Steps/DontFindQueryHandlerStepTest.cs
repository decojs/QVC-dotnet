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
    public class DontFindQueryHandlerStepTest
    {
        private IFindQueryHandlerStep _step;

        [SetUp]
        public void Setup()
        {
            _step = new DontFindQueryHandlerStep(new Exception("blabla"));
        }

        [Test]
        public void Test()
        {
            var spy = Substitute.For<Func<IQuery, Type>>();
            _step.FindQueryHandler(spy).ShouldBeOfType<DontCreateQueryHandlerStep>();
            spy.DidNotReceive().Invoke(Arg.Any<IQuery>());
        }
    }
}