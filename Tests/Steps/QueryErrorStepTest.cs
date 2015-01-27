using System;
using NSubstitute;
using NUnit.Framework;
using Qvc;
using Qvc.Executables;
using Qvc.Handlers;
using Qvc.Results;
using Qvc.Steps;
using Qvc.Steps.Implementations;
using Shouldly;

namespace Tests.Steps
{
    [TestFixture]
    public class QueryErrorStepTest
    {
        private Exception _exception;
        private IJsonAndQueryType _jsonAndQueryType;
        private IFindQueryHandlerStep _findQueryHandlerStep;
        private ICreateQueryHandlerStep _createQueryHandlerStep;
        private IExecuteQueryStep _executeQueryStep;

        [SetUp]
        public void Setup()
        {
            _exception = new Exception("blabla");
            _jsonAndQueryType = new QueryErrorStep(new QueryResult(_exception));
            _findQueryHandlerStep = new QueryErrorStep(new QueryResult(_exception));
            _createQueryHandlerStep = new QueryErrorStep(new QueryResult(_exception));
            _executeQueryStep = new QueryErrorStep(new QueryResult(_exception));
        }

        [Test]
        public void TestDeserializeQuery()
        {
            var spy = Substitute.For<Func<string, Type, object>>();
            _jsonAndQueryType.DeserializeQuery(spy).ShouldBeOfType<QueryErrorStep>();
            spy.DidNotReceive().Invoke(Arg.Any<string>(), Arg.Any<Type>());
        }

        [Test]
        public void TestFindQueryHandler()
        {
            var spy = Substitute.For<Func<IQuery, Type>>();
            _findQueryHandlerStep.FindQueryHandler(spy).ShouldBeOfType<QueryErrorStep>();
            spy.DidNotReceive().Invoke(Arg.Any<IQuery>());
        }
        
        [Test]
        public void TestCreateQueryHandler()
        {
            var spy = Substitute.For<Func<Type, object>>();
            _createQueryHandlerStep.CreateQueryHandler(spy).ShouldBeOfType<QueryErrorStep>();
            spy.DidNotReceive().Invoke(Arg.Any<Type>());
        }

        [Test]
        public void TestExecuteQuery()
        {
            var spy = Substitute.For<Func<IHandleExecutable, IQuery, object>>();
            _executeQueryStep.HandleQuery(spy).Serialize(r =>
            {
                r.ShouldBeOfType(typeof(QueryResult));
                r.Success.ShouldBe(false);
                r.Valid.ShouldBe(true);
                r.Exception.ShouldBe(_exception);
                ((QueryResult)r).Result.ShouldBe(null);
                return "";
            });
        }
    }
}