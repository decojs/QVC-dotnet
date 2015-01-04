using NUnit.Framework;
using Qvc.Exceptions;
using Qvc.Executables;
using Qvc.Steps;
using Qvc.Steps.Implementations;
using Shouldly;
using Tests.Executables;

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

        [Test]
        public void TestHandlerDoesNotExist()
        {
            _step.FindQueryHandler(q =>
            {
                throw new QueryHandlerDoesNotExistException(q.GetType().FullName);
            }).ShouldBeOfType<QueryErrorStep>();
        }

        [Test]
        public void TestDuplicateHandler()
        {
            _step.FindQueryHandler(q =>
            {
                throw new DuplicateQueryHandlerException(q.GetType().FullName);
            }).ShouldBeOfType<QueryErrorStep>();
        }
    }
}