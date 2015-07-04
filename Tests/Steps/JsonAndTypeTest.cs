using NUnit.Framework;
using Qvc;
using Qvc.Steps;
using Shouldly;
using Tests.Executables;

namespace Tests.Steps
{
    [TestFixture]
    public class JsonAndTypeTest
    {
        private JsonAndType _jsonAndQueryType;

        private JsonAndType _jsonAndCommandType;

        [SetUp]
        public void Setup()
        {
            _jsonAndCommandType = new JsonAndType("json", typeof(CommandA));
            _jsonAndQueryType = new JsonAndType("json", typeof(QueryA));
        }
        
        [Test]
        public void TestCommand()
        {
            CommandSteps.DeserializeCommand(_jsonAndCommandType, (j, t) =>
            {
                j.ShouldBe("json");
                t.ShouldBe(typeof(CommandA));
                return new CommandA();
            }).ShouldBeOfType<CommandA>();
        }

        [Test]
        public void TestQuery()
        {
            QuerySteps.DeserializeQuery(_jsonAndQueryType, (j, t) =>
            {
                j.ShouldBe("json");
                t.ShouldBe(typeof(QueryA));
                return new QueryA();
            }).ShouldBeOfType<QueryA>();
        }
    }
}