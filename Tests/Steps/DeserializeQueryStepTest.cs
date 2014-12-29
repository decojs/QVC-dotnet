using NUnit.Framework;
using Qvc.Steps;
using Qvc.Steps.Implementations;
using Shouldly;
using Tests.Executables;

namespace Tests.Steps
{
    [TestFixture]
    public class DeserializeQueryStepTest
    {
        private IDeserializeQueryStep _step;

        [SetUp]
        public void Setup()
        {
            _step = new DeserializeQueryStep("json", typeof(QueryA));
        }

        [Test]
        public void Test()
        {
            _step.DeserializeQuery((j, t) =>
            {
                j.ShouldBe("json");
                t.ShouldBe(typeof(QueryA));
                return new QueryA();
            }).ShouldBeOfType<FindQueryHandlerStep>();
        }
    }
}