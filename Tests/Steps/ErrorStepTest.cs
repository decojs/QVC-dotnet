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
    public class ErrorStepTest
    {
        private Exception _exception;
        private IDeserializeQueryStep _deserializeQueryStep;
        private IDeserializeCommandStep _deserializeCommandStep;
        private IFindCommandHandlerStep _findCommandHandlerStep;
        private IFindQueryHandlerStep _findQueryHandlerStep;
        private ICreateCommandHandlerStep _createCommandHandlerStep;
        private ICreateQueryHandlerStep _createQueryHandlerStep;
        private IExecuteCommandStep _executeCommandStep;
        private IExecuteQueryStep _executeQueryStep;

        [SetUp]
        public void Setup()
        {
            _exception = new Exception("blabla");
            _deserializeQueryStep = new ErrorStep(_exception);
            _deserializeCommandStep = new ErrorStep(_exception);
            _findCommandHandlerStep = new ErrorStep(_exception);
            _findQueryHandlerStep = new ErrorStep(_exception);
            _createCommandHandlerStep = new ErrorStep(_exception);
            _createQueryHandlerStep = new ErrorStep(_exception);
            _executeCommandStep = new ErrorStep(_exception);
            _executeQueryStep = new ErrorStep(_exception);
        }

        [Test]
        public void TestDeserializeQuery()
        {
            var spy = Substitute.For<Func<string, Type, object>>();
            _deserializeQueryStep.DeserializeQuery(spy).ShouldBeOfType<ErrorStep>();
            spy.DidNotReceive().Invoke(Arg.Any<string>(), Arg.Any<Type>());
        }

        [Test]
        public void TestDeserializeCommand()
        {
            var spy = Substitute.For<Func<string, Type, object>>();
            _deserializeCommandStep.DeserializeCommand(spy).ShouldBeOfType<ErrorStep>();
            spy.DidNotReceive().Invoke(Arg.Any<string>(), Arg.Any<Type>());
        }

        [Test]
        public void TestFindCommandHandler()
        {
            var spy = Substitute.For<Func<ICommand, Type>>();
            _findCommandHandlerStep.FindCommandHandler(spy).ShouldBeOfType<ErrorStep>();
            spy.DidNotReceive().Invoke(Arg.Any<ICommand>());
        }

        [Test]
        public void TestFindQueryHandler()
        {
            var spy = Substitute.For<Func<IQuery, Type>>();
            _findQueryHandlerStep.FindQueryHandler(spy).ShouldBeOfType<ErrorStep>();
            spy.DidNotReceive().Invoke(Arg.Any<IQuery>());
        }

        [Test]
        public void TestCreateCommandHandler()
        {
            var spy = Substitute.For<Func<Type, object>>();
            _createCommandHandlerStep.CreateCommandHandler(spy).ShouldBeOfType<ErrorStep>();
            spy.DidNotReceive().Invoke(Arg.Any<Type>());
        }

        [Test]
        public void TestCreateQueryHandler()
        {
            var spy = Substitute.For<Func<Type, object>>();
            _createQueryHandlerStep.CreateQueryHandler(spy).ShouldBeOfType<ErrorStep>();
            spy.DidNotReceive().Invoke(Arg.Any<Type>());
        }

        [Test]
        public void TestExecuteCommand()
        {
            var spy = Substitute.For<Action<IHandleExecutable, ICommand>>();
            _executeCommandStep.HandleCommand(spy).Serialize(r =>
            {
                r.ShouldBeOfType(typeof(CommandResult));
                r.Success.ShouldBe(false);
                r.Valid.ShouldBe(true);
                r.Exception.ShouldBe(_exception);
                return "";
            });
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