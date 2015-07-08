using System;
using NUnit.Framework;
using Qvc;
using Qvc.Steps;
using Shouldly;

using Tests.TestMaterial;

namespace Tests.Steps
{
    [TestFixture]
    public class CreateQueryHandlerStepTest
    {
        private QueryAndHandlerType _step;

        private QueryB _query;

        [SetUp]
        public void Setup()
        {
            _query = new QueryB();
            _step = new QueryAndHandlerType(_query, typeof(QueryHandlerB));
        }

        [Test]
        public void Test()
        {
            var result = QuerySteps.CreateQueryHandler(_step, h =>
            {
                h.ShouldBe(typeof(QueryHandlerB));
                return new QueryHandlerB();
            });

            result.Handler.ShouldBeOfType<QueryHandlerB>();
            result.Query.ShouldBe(_query);
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