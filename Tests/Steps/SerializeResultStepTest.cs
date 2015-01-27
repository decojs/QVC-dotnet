using NUnit.Framework;
using Qvc;
using Qvc.Results;
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
            result.Serialize(r =>
            {
                r.ShouldBe(result);
                return "result";
            }).ShouldBe("result");
        }

        [Test]
        public void TestQueryResult()
        {
            var result = new QueryResult("result");
            result.Serialize(r =>
            {
                r.ShouldBe(result);
                return r.Result as string;
            }).ShouldBe("result");
        }
    }
}