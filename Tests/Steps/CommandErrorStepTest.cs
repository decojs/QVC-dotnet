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
    public class CommandErrorStepTest
    {
        private Exception _exception;
        private IJsonAndCommandType _jsonAndCommandType;
        private ICommand _findCommandHandlerStep;
        private ICommandAndHandlerType _commandAndHandlerType;
        private ICommandAndHandler _commandAndHandler;

        [SetUp]
        public void Setup()
        {
            _exception = new Exception("blabla");
            _jsonAndCommandType = new CommandResult(_exception);
            _findCommandHandlerStep = new CommandResult(_exception);
            _commandAndHandlerType = new CommandResult(_exception);
            _commandAndHandler = new CommandResult(_exception);
        }

        [Test]
        public void TestDeserializeCommand()
        {
            var spy = Substitute.For<Func<string, Type, object>>();
            _jsonAndCommandType.DeserializeCommand(spy).ShouldBeOfType<CommandResult>();
            spy.DidNotReceive().Invoke(Arg.Any<string>(), Arg.Any<Type>());
        }

        [Test]
        public void TestFindCommandHandler()
        {
            var spy = Substitute.For<Func<ICommand, Type>>();
            _findCommandHandlerStep.FindCommandHandler(spy).ShouldBeOfType<CommandResult>();
            spy.DidNotReceive().Invoke(Arg.Any<ICommand>());
        }

        [Test]
        public void TestCreateCommandHandler()
        {
            var spy = Substitute.For<Func<Type, object>>();
            _commandAndHandlerType.CreateCommandHandler(spy).ShouldBeOfType<CommandResult>();
            spy.DidNotReceive().Invoke(Arg.Any<Type>());
        }

        [Test]
        public void TestExecuteCommand()
        {
            var spy = Substitute.For<Action<IHandleExecutable, ICommand>>();
            _commandAndHandler.HandleCommand(spy).Serialize(r =>
            {
                r.ShouldBeOfType(typeof(CommandResult));
                r.Success.ShouldBe(false);
                r.Valid.ShouldBe(true);
                r.Exception.ShouldBe(_exception);
                return "";
            });
        }
    }
}