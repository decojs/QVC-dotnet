using NUnit.Framework;
using Qvc;
using Qvc.Exceptions;
using Qvc.Executables;
using Qvc.Results;
using Qvc.Steps;
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
            var result = QuerySteps.FindQueryHandler(_query, q =>
            {
                q.ShouldBe(_query);
                return typeof(QueryHandlerB);
            });

            result.HandlerType.ShouldBe(typeof(QueryHandlerB));
            result.Query.ShouldBe(_query);
        }

        [Test]
        public void TestHandlerDoesNotExist()
        {
            Should.Throw<QueryHandlerDoesNotExistException>(() =>
                QuerySteps.FindQueryHandler(_query, q =>
                {
                    throw new QueryHandlerDoesNotExistException(q.GetType().FullName);
                }));
        }

        [Test]
        public void TestDuplicateHandler()
        {
            Should.Throw<DuplicateQueryHandlerException>(() =>
                QuerySteps.FindQueryHandler(_query, q =>
                {
                    throw new DuplicateQueryHandlerException(q.GetType().FullName);
                }));
        }
    }
}