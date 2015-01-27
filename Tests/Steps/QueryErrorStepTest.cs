using System;
using NSubstitute;
using NUnit.Framework;
using Qvc;
using Qvc.Executables;
using Qvc.Handlers;
using Qvc.Results;
using Qvc.Steps;
using Shouldly;

namespace Tests.Steps
{
    [TestFixture]
    public class QueryErrorStepTest
    {
        private Exception _exception;
        private IJsonAndQueryType _jsonAndQueryType;
        private IQuery _findQueryHandlerStep;
        private IQueryAndHandlerType _queryAndHandlerType;
        private IQueryAndHandler _queryAndHandler;

        [SetUp]
        public void Setup()
        {
            _exception = new Exception("blabla");
            _jsonAndQueryType = new QueryResult(_exception);
            _findQueryHandlerStep = new QueryResult(_exception);
            _queryAndHandlerType = new QueryResult(_exception);
            _queryAndHandler = new QueryResult(_exception);
        }

        [Test]
        public void TestDeserializeQuery()
        {
            var spy = Substitute.For<Func<string, Type, object>>();
            _jsonAndQueryType.DeserializeQuery(spy).ShouldBeOfType<QueryResult>();
            spy.DidNotReceive().Invoke(Arg.Any<string>(), Arg.Any<Type>());
        }

        [Test]
        public void TestFindQueryHandler()
        {
            var spy = Substitute.For<Func<IQuery, Type>>();
            _findQueryHandlerStep.FindQueryHandler(spy).ShouldBeOfType<QueryResult>();
            spy.DidNotReceive().Invoke(Arg.Any<IQuery>());
        }
        
        [Test]
        public void TestCreateQueryHandler()
        {
            var spy = Substitute.For<Func<Type, object>>();
            _queryAndHandlerType.CreateQueryHandler(spy).ShouldBeOfType<QueryResult>();
            spy.DidNotReceive().Invoke(Arg.Any<Type>());
        }

        [Test]
        public void TestExecuteQuery()
        {
            var spy = Substitute.For<Func<IHandleExecutable, IQuery, object>>();
            _queryAndHandler.HandleQuery(spy).Serialize(r =>
            {
                r.ShouldBeOfType(typeof(QueryResult));
                r.Success.ShouldBe(false);
                r.Valid.ShouldBe(true);
                r.Exception.ShouldBe(_exception);
                r.Result.ShouldBe(null);
                return "";
            });
        }
    }
}