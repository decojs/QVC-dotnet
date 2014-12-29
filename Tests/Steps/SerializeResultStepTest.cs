using NUnit.Framework;
using Qvc.Results;
using Qvc.Steps.Implementations;
using Shouldly;

namespace Tests.Steps
{
    [TestFixture]
    public class SerializeResultStepTest
    {
        private SerializeResultStep _step;
        private ExecutableResult _result;

        [SetUp]
        public void Setup()
        {
            _result = new ExecutableResult();
            _step = new SerializeResultStep(_result);
        }

        [Test]
        public void Test()
        {
            _step.Serialize(r =>
            {
                r.ShouldBe(_result);
                return "result";
            }).ShouldBe("result");
        }
    }
}