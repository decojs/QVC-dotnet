using System;
using System.Threading.Tasks;

using NUnit.Framework;
using Qvc;
using Qvc.Executables;
using Qvc.Handlers;
using Qvc.Steps;
using Shouldly;

using Tests.TestMaterial;

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
        public async Task Test()
        {
            var result = await QuerySteps.HandleQuery(_step, (h, c) =>
            {
                h.ShouldBe(_handler);
                c.ShouldBe(_query);
                return Task.FromResult((object)40);
            });

            result.Success.ShouldBe(true);
            result.Valid.ShouldBe(true);
            result.Exception.ShouldBe(null);
            result.Result.ShouldBe(40);
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
        public void TestWhenThrowsAsync()
        {
            Should.Throw<NullReferenceException>(() =>
                QuerySteps.HandleQuery(_step, async (h, c) =>
                {
                    throw new NullReferenceException();
                }));
        }
    }
}