using System;
using NUnit.Framework;
using Qvc.Executables;
using Qvc.Handlers;
using Qvc.Results;
using Qvc.Steps;
using Qvc.Steps.Implementations;
using Shouldly;
using Tests.Executables;
using Tests.Repository;

namespace Tests.Steps
{
    [TestFixture]
    public class ExecuteQueryStepTest
    {
        private IExecuteQueryStep _step;
        private IQuery _query;
        private IHandleExecutable _handler;

        [SetUp]
        public void Setup()
        {
            _query = new QueryB();
            _handler = new QueryHandlerB();
            _step = new ExecuteQueryStep(_query, _handler);
        }

        [Test]
        public void Test()
        {
            _step.HandleQuery((h, c) =>
            {
                h.ShouldBe(_handler);
                c.ShouldBe(_query);
                return 40;
            }).Serialize(r =>
            {
                r.ShouldBeOfType(typeof(QueryResult));
                r.Success.ShouldBe(true);
                r.Valid.ShouldBe(true);
                r.Exception.ShouldBe(null);
                ((QueryResult) r).Result.ShouldBe(40);
                return "";
            });
        }

        [Test]
        public void TestWhenThrows()
        {
            _step.HandleQuery((h, c) =>
            {
                throw new NullReferenceException();
            }).Serialize(r =>
            {
                r.ShouldBeOfType(typeof(QueryResult));
                r.Success.ShouldBe(false);
                r.Valid.ShouldBe(true);
                r.Exception.ShouldBeOfType(typeof(NullReferenceException));
                ((QueryResult)r).Result.ShouldBe(null);
                return "";
            });
        }

        [Test]
        public void TestWhenThrowsInvocationException()
        {
            _step.HandleQuery(Qvc.Default.HandleQuery).Serialize(r =>
            {
                r.ShouldBeOfType(typeof(QueryResult));
                r.Success.ShouldBe(false);
                r.Valid.ShouldBe(true);
                r.Exception.ShouldBeOfType(typeof(NullReferenceException));
                ((QueryResult)r).Result.ShouldBe(null);
                return "";
            });
        }
    }
}