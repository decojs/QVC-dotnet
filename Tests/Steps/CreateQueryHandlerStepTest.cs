using System;
using NUnit.Framework;
using Qvc;
using Qvc.Steps;
using Shouldly;
using Tests.Executables;

namespace Tests.Steps
{
    [TestFixture]
    public class CreateQueryHandlerStepTest
    {
        private QueryAndHandlerType _step;

        [SetUp]
        public void Setup()
        {
            _step = new QueryAndHandlerType(new QueryB(), typeof(QueryHandlerB));
        }

        [Test]
        public void Test()
        {
            QuerySteps.CreateQueryHandler(_step, h =>
            {
                h.ShouldBe(typeof(QueryHandlerB));
                return new QueryHandlerB();
            }).ShouldBeOfType<QueryAndHandler>();
        }

        [Test]
        public void TestThrowsException()
        {
            Should.Throw<Exception>(() =>
                QuerySteps.CreateQueryHandler(_step, h =>
                {
                    throw new Exception("could not be made");
                }));
        }
    }
}