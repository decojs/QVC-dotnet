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
    public class CommandErrorStepTest
    {
        private Exception _exception;
        private IDeserializeCommandStep _deserializeCommandStep;
        private IFindCommandHandlerStep _findCommandHandlerStep;
        private ICreateCommandHandlerStep _createCommandHandlerStep;
        private IExecuteCommandStep _executeCommandStep;

        [SetUp]
        public void Setup()
        {
            _exception = new Exception("blabla");
            _deserializeCommandStep = new CommandErrorStep(new CommandResult(_exception));
            _findCommandHandlerStep = new CommandErrorStep(new CommandResult(_exception));
            _createCommandHandlerStep = new CommandErrorStep(new CommandResult(_exception));
            _executeCommandStep = new CommandErrorStep(new CommandResult(_exception));
        }

        [Test]
        public void TestDeserializeCommand()
        {
            var spy = Substitute.For<Func<string, Type, object>>();
            _deserializeCommandStep.DeserializeCommand(spy).ShouldBeOfType<CommandErrorStep>();
            spy.DidNotReceive().Invoke(Arg.Any<string>(), Arg.Any<Type>());
        }

        [Test]
        public void TestFindCommandHandler()
        {
            var spy = Substitute.For<Func<ICommand, Type>>();
            _findCommandHandlerStep.FindCommandHandler(spy).ShouldBeOfType<CommandErrorStep>();
            spy.DidNotReceive().Invoke(Arg.Any<ICommand>());
        }

        [Test]
        public void TestCreateCommandHandler()
        {
            var spy = Substitute.For<Func<Type, object>>();
            _createCommandHandlerStep.CreateCommandHandler(spy).ShouldBeOfType<CommandErrorStep>();
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
    }
}