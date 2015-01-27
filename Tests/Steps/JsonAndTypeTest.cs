using NUnit.Framework;
using Qvc;
using Qvc.Steps;
using Qvc.Steps.Implementations;
using Shouldly;
using Tests.Executables;

namespace Tests.Steps
{
    [TestFixture]
    public class JsonAndTypeTest
    {
        private IJsonAndQueryType _jsonAndQueryType;

        private IJsonAndCommandType _jsonAndCommandType;

        [SetUp]
        public void Setup()
        {
            _jsonAndCommandType = new JsonAndType("json", typeof(CommandA));
            _jsonAndQueryType = new JsonAndType("json", typeof(QueryA));
        }
        
        [Test]
        public void TestCommand()
        {
            _jsonAndCommandType.DeserializeCommand((j, t) =>
            {
                j.ShouldBe("json");
                t.ShouldBe(typeof(CommandA));
                return new CommandA();
            }).ShouldBeOfType<CommandA>();
        }

        [Test]
        public void TestQuery()
        {
            _jsonAndQueryType.DeserializeQuery((j, t) =>
            {
                j.ShouldBe("json");
                t.ShouldBe(typeof(QueryA));
                return new QueryA();
            }).ShouldBeOfType<QueryA>();
        }
    }
}