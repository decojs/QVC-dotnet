using NUnit.Framework;
using Qvc;
using Qvc.Exceptions;
using Qvc.Results;
using Qvc.Steps;
using Shouldly;
using Tests.Executables;

namespace Tests.Steps
{
    [TestFixture]
    public class QueryNameAndJsonTest
    {
        private QueryNameAndJson _step;

        [SetUp]
        public void Setup()
        {
            _step = new QueryNameAndJson("name", "json");
        }

        [Test]
        public void TestQuery()
        {
            QuerySteps.FindQuery(_step, name =>
            {
                name.ShouldBe("name");
                return typeof(QueryA);
            });
        }

        [Test]
        public void TestQueryDoesNotExist()
        {
            Should.Throw<QueryDoesNotExistException>(() =>
                QuerySteps.FindQuery(_step, name =>
                {
                    throw new QueryDoesNotExistException(name);
                }));
        }

        [Test]
        public void TestQueryExecutableDoesNotExist()
        {
            Should.Throw<ExecutableDoesNotExistException>(() =>
                QuerySteps.FindQuery(_step, name =>
                {
                    throw new ExecutableDoesNotExistException(name);
                }));
        }
    }
}