using NUnit.Framework;
using Qvc;
using Qvc.Results;
using Qvc.Steps;

using Shouldly;

namespace Tests.Steps
{
    [TestFixture]
    public class SerializeResultStepTest
    {
        [Test]
        public void TestCommandResult()
        {
            var result = new CommandResult();
            CommandSteps.Serialize(result, r =>
            {
                r.ShouldBe(result);
                return "result";
            }).ShouldBe("result");
        }

        [Test]
        public void TestQueryResult()
        {
            var result = new QueryResult("result");
            QuerySteps.Serialize(result, r =>
            {
                r.ShouldBe(result);
                return r.Result as string;
            }).ShouldBe("result");
        }
    }
}