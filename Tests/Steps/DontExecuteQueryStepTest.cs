using System;
using NSubstitute;
using NUnit.Framework;
using Qvc.Executables;
using Qvc.Handlers;
using Qvc.Results;
using Qvc.Steps;
using Qvc.Steps.Implementations;
using Shouldly;

namespace Tests.Steps
{
    [TestFixture]
    public class DontExecuteQueryStepTest
    {
        private IExecuteQueryStep _step;
        private Exception _exception;

        [SetUp]
        public void Setup()
        {
            _exception = new Exception("blabla");
            _step = new DontExecuteQueryStep(_exception);
        }

        [Test]
        public void Test()
        {
            var spy = Substitute.For<Func<IHandleExecutable, IQuery, object>>();;
            _step.HandleQuery(spy).Serialize(r =>
            {
                r.ShouldBeOfType(typeof(QueryResult));
                r.Success.ShouldBe(false);
                r.Valid.ShouldBe(true);
                r.Exception.ShouldBe(_exception);
                ((QueryResult) r).Result.ShouldBe(null);
                return "";
            });
        }
    }
}