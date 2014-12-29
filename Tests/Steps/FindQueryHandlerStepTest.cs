using NUnit.Framework;
using Qvc.Executables;
using Qvc.Steps;
using Qvc.Steps.Implementations;
using Shouldly;
using Tests.Executables;
using Tests.Repository;

namespace Tests.Steps
{
    [TestFixture]
    public class FindQueryHandlerStepTest
    {
        private IFindQueryHandlerStep _step;
        private IQuery _query;

        [SetUp]
        public void Setup()
        {
            _query = new QueryB();
            _step = new FindQueryHandlerStep(_query);
        }

        [Test]
        public void Test()
        {
            _step.FindQueryHandler(q =>
            {
                q.ShouldBe(_query);
                return typeof(QueryHandlerB);
            }).ShouldBeOfType<CreateQueryHandlerStep>();
        }
    }
}