using NUnit.Framework;
using Qvc.Steps;
using Shouldly;
using Tests.Executables;

namespace Tests.Steps
{
    [TestFixture]
    public class DeserializeCommandStepTest
    {
        private DeserializeCommandStep _step;

        [SetUp]
        public void Setup()
        {
            _step = new DeserializeCommandStep("json", typeof(CommandA));
        }

        [Test]
        public void Test()
        {
            _step.DeserializeCommand((j, t) =>
            {
                j.ShouldBe("json");
                t.ShouldBe(typeof(CommandA));
                return new CommandA();
            });
        }
    }
}