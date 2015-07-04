using System;
using NUnit.Framework;
using Qvc;
using Qvc.Executables;
using Qvc.Handlers;
using Qvc.Results;
using Qvc.Steps;
using Shouldly;
using Tests.Executables;

namespace Tests.Steps
{
    [TestFixture]
    public class ExecuteQueryStepTest
    {
        private QueryAndHandler _step;
        private IQuery _query;
        private IHandleExecutable _handler;

        [SetUp]
        public void Setup()
        {
            _query = new QueryB();
            _handler = new QueryHandlerB();
            _step = new QueryAndHandler(_query, _handler);
        }

        [Test]
        public void Test()
        {
            var result = QuerySteps.HandleQuery(_step, (h, c) =>
            {
                h.ShouldBe(_handler);
                c.ShouldBe(_query);
                return 40;
            });

            QuerySteps.Serialize(result, r =>
            {
                r.ShouldBeOfType(typeof(QueryResult));
                r.Success.ShouldBe(true);
                r.Valid.ShouldBe(true);
                r.Exception.ShouldBe(null);
                r.Result.ShouldBe(40);
                return "";
            });
        }

        [Test]
        public void TestWhenThrows()
        {
            Should.Throw<NullReferenceException>(() =>
                QuerySteps.HandleQuery(_step, (h, c) =>
                {
                    throw new NullReferenceException();
                }));
        }

        [Test]
        public void TestWhenThrowsInvocationException()
        {
            Should.Throw<NullReferenceException>(() => QuerySteps.HandleQuery(_step, Qvc.Default.HandleQuery));
        }
    }
}