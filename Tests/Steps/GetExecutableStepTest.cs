using NUnit.Framework;
using Qvc.Exceptions;
using Qvc.Steps.Implementations;
using Shouldly;
using Tests.Executables;

namespace Tests.Steps
{
    [TestFixture]
    public class GetExecutableStepTest
    {
        private GetExecutableStep _step;

        [SetUp]
        public void Setup()
        {
            _step = new GetExecutableStep("name", "json");
        }

        [Test]
        public void TestCommand()
        {
            _step.GetCommand(name =>
            {
                name.ShouldBe("name");
                return typeof(CommandA);
            });
        }

        [Test]
        public void TestQuery()
        {
            _step.GetQuery(name =>
            {
                name.ShouldBe("name");
                return typeof(QueryA);
            });
        }

        [Test]
        public void TestCommandDoesNotExist()
        {
            _step.GetCommand(name =>
            {
                throw new CommandDoesNotExistException(name);
            }).ShouldBeOfType<ErrorStep>();
        }

        [Test]
        public void TestQueryDoesNotExist()
        {
            _step.GetQuery(name =>
            {
                throw new QueryDoesNotExistException(name);
            }).ShouldBeOfType<ErrorStep>();
        }

        [Test]
        public void TestQueryExecutableDoesNotExist()
        {
            _step.GetQuery(name =>
            {
                throw new ExecutableDoesNotExistException(name);
            }).ShouldBeOfType<ErrorStep>();
        }

        [Test]
        public void TestCommandExecutableDoesNotExist()
        {
            _step.GetCommand(name =>
            {
                throw new ExecutableDoesNotExistException(name);
            }).ShouldBeOfType<ErrorStep>();
        }
    }
}