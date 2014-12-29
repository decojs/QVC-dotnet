using System;
using NUnit.Framework;
using Qvc.Steps;
using Qvc.Steps.Implementations;
using Shouldly;
using Tests.Executables;

namespace Tests.Steps
{
    [TestFixture]
    public class CreateQueryHandlerStepTest
    {
        private ICreateQueryHandlerStep _step;

        [SetUp]
        public void Setup()
        {
            _step = new CreateQueryHandlerStep(new QueryB(), typeof(QueryHandlerB));
        }

        [Test]
        public void Test()
        {
            _step.CreateQueryHandler(h =>
            {
                h.ShouldBe(typeof(QueryHandlerB));
                return new QueryHandlerB();
            }).ShouldBeOfType<ExecuteQueryStep>();
        }

        [Test]
        public void TestThrowsException()
        {
            _step.CreateQueryHandler(h =>
            {
                throw new Exception("could not be made");
            }).ShouldBeOfType<DontExecuteQueryStep>();
        }
    }
}