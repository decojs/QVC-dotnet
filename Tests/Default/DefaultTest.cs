using NSubstitute;
using NUnit.Framework;
using Qvc.Handlers;
using Shouldly;
using Tests.Executables;

namespace Tests.Default
{
    [TestFixture]
    public class DefaultTest
    {
        private IHandleCommand<CommandC> _commandHandler;
        private IHandleQuery<QueryC, string> _queryHandler;
        private CommandC _command;
        private QueryC _query;

        [SetUp]
        public void Setup()
        {
            _commandHandler = Substitute.For<IHandleCommand<CommandC>>();
            _queryHandler = Substitute.For<IHandleQuery<QueryC, string>>();
            _command = new CommandC();
            _query = new QueryC();

            _queryHandler.Handle(_query).Returns("hello");
        }

        [Test]
        public void TestCommand()
        {
            Qvc.Default.HandleCommand(_commandHandler, _command);
            _commandHandler.Received().Handle(_command);
        }

        [Test]
        public void TestQuery()
        {
            var result = Qvc.Default.HandleQuery(_queryHandler, _query);
            _queryHandler.Received().Handle(_query);
            result.ShouldBe("hello");
        }
    }
}