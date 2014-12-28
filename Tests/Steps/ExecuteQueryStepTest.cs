using NUnit.Framework;
using Qvc.Executables;
using Qvc.Handlers;
using Qvc.Steps;
using Shouldly;
using Tests.Executables;
using Tests.Repository;

namespace Tests.Steps
{
    [TestFixture]
    public class ExecuteQueryStepTest
    {
        private ExecuteQueryStep _step;
        private IQuery _query;
        private IHandleExecutable _handler;

        [SetUp]
        public void Setup()
        {
            _query = new QueryB();
            _handler = new QueryHandlerB();
            _step = new ExecuteQueryStep(_query, _handler);
        }

        [Test]
        public void Test()
        {
            _step.HandleQuery((h, c) =>
            {
                h.ShouldBe(_handler);
                c.ShouldBe(_query);
                return 40;
            });
        }
    }
}