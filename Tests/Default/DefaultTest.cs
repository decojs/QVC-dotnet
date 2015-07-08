﻿using System.Threading.Tasks;

using NSubstitute;
using NUnit.Framework;
using Qvc.Handlers;
using Shouldly;

using Tests.TestMaterial;

namespace Tests.Default
{
    [TestFixture]
    public class DefaultTest
    {
        private IHandleCommand<CommandC> _commandHandler;
        private IHandleCommandAsync<CommandC> _asyncCommandHandler;
        private IHandleQuery<QueryC, string> _queryHandler;
        private IHandleQueryAsync<QueryC, string> _asyncQueryHandler; 
        private CommandC _command;
        private QueryC _query;

        [SetUp]
        public void Setup()
        {
            _commandHandler = Substitute.For<IHandleCommand<CommandC>>();
            _asyncCommandHandler = Substitute.For<IHandleCommandAsync<CommandC>>();
            _queryHandler = Substitute.For<IHandleQuery<QueryC, string>>();
            _asyncQueryHandler = Substitute.For<IHandleQueryAsync<QueryC, string>>();
            _command = new CommandC();
            _query = new QueryC();

            _queryHandler.Handle(_query).Returns("hello");
            _asyncQueryHandler.Handle(_query).Returns(Task.FromResult("hello"));
        }

        [Test]
        public async void TestCommand()
        {
            await Qvc.Default.HandleCommand(_commandHandler, _command);
            _commandHandler.Received().Handle(_command);
        }

        [Test]
        public async void TestAsyncCommand()
        {
            await Qvc.Default.HandleCommand(_asyncCommandHandler, _command);
            _asyncCommandHandler.Received().Handle(_command);
        }

        [Test]
        public async void TestQuery()
        {
            var result = await Qvc.Default.HandleQuery(_queryHandler, _query);
            _queryHandler.Received().Handle(_query);
            result.ShouldBe("hello");
        }

        [Test]
        public async void TestQueryAsync()
        {
            var result = await Qvc.Default.HandleQuery(_asyncQueryHandler, _query);
            _asyncQueryHandler.Received().Handle(_query);
            result.ShouldBe("hello");
        }
    }
}