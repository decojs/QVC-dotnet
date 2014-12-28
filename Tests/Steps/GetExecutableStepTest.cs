using NUnit.Framework;
using Qvc.Steps;
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
                return typeof (CommandA);
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
    }
}