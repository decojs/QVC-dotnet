using NUnit.Framework;
using Qvc.Steps;
using Qvc.Steps.Implementations;
using Shouldly;
using Tests.Executables;
using Tests.Repository;

namespace Tests.Steps
{
    [TestFixture]
    public class CreateQueryHandlerStepTest
    {
        private ICreateQueryHandlerStep _step;

        [SetUp]
        public void Setup()
        {
            _step = new CreateQueryHandlerStep(new QueryB(), typeof(QueryHandlerB));
        }

        [Test]
        public void Test()
        {
            _step.CreateQueryHandler(h =>
            {
                h.ShouldBe(typeof(QueryHandlerB));
                return new QueryHandlerB();
            }).ShouldBeOfType<ExecuteQueryStep>();
        }
    }
}