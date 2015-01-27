using System;
using NUnit.Framework;
using Qvc;
using Qvc.Steps;
using Qvc.Steps.Implementations;
using Shouldly;
using Tests.Executables;

namespace Tests.Steps
{
    [TestFixture]
    public class CreateQueryHandlerStepTest
    {
        private IQueryAndHandlerType _step;

        [SetUp]
        public void Setup()
        {
            _step = new QueryAndHandlerType(new QueryB(), typeof(QueryHandlerB));
        }

        [Test]
        public void Test()
        {
            _step.CreateQueryHandler(h =>
            {
                h.ShouldBe(typeof(QueryHandlerB));
                return new QueryHandlerB();
            }).ShouldBeOfType<QueryAndHandler>();
        }

        [Test]
        public void TestThrowsException()
        {
            _step.CreateQueryHandler(h =>
            {
                throw new Exception("could not be made");
            }).ShouldBeOfType<QueryErrorStep>();
        }
    }
}