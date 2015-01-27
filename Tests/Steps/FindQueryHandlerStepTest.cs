using NUnit.Framework;
using Qvc;
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
        private IQuery _query;

        [SetUp]
        public void Setup()
        {
            _query = new QueryB();
        }

        [Test]
        public void Test()
        {
            _query.FindQueryHandler(q =>
            {
                q.ShouldBe(_query);
                return typeof(QueryHandlerB);
            }).ShouldBeOfType<CreateQueryHandlerStep>();
        }

        [Test]
        public void TestHandlerDoesNotExist()
        {
            _query.FindQueryHandler(q =>
            {
                throw new QueryHandlerDoesNotExistException(q.GetType().FullName);
            }).ShouldBeOfType<QueryErrorStep>();
        }

        [Test]
        public void TestDuplicateHandler()
        {
            _query.FindQueryHandler(q =>
            {
                throw new DuplicateQueryHandlerException(q.GetType().FullName);
            }).ShouldBeOfType<QueryErrorStep>();
        }
    }
}