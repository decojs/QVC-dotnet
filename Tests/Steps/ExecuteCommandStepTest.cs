using System;
using NUnit.Framework;
using Qvc;
using Qvc.Executables;
using Qvc.Handlers;
using Qvc.Results;
using Qvc.Steps;
using Qvc.Steps.Implementations;
using Shouldly;
using Tests.Executables;

namespace Tests.Steps
{
    [TestFixture]
    public class ExecuteCommandStepTest
    {
        private ICommandAndHandler _step;
        private ICommand _command;
        private IHandleExecutable _handler;

        [SetUp]
        public void Setup()
        {
            _command = new CommandB();
            _handler = new CommandHandlerB();
            _step = new CommandAndHandler(_command, _handler);
        }

        [Test]
        public void Test()
        {
            _step.HandleCommand((h, c) =>
            {
                h.ShouldBe(_handler);
                c.ShouldBe(_command);
            }).Serialize(r =>
            {
                r.ShouldBeOfType(typeof(CommandResult));
                r.Success.ShouldBe(true);
                r.Valid.ShouldBe(true);
                r.Exception.ShouldBe(null);
                return "";
            });
        }

        [Test]
        public void TestWhenThrows()
        {
            _step.HandleCommand((h, c) =>
            {
                throw new NullReferenceException();
            }).Serialize(r =>
            {
                r.ShouldBeOfType(typeof(CommandResult));
                r.Success.ShouldBe(false);
                r.Valid.ShouldBe(true);
                r.Exception.ShouldBeOfType(typeof(NullReferenceException));
                return "";
            });
        }

        [Test]
        public void TestWhenThrowsInvocationException()
        {
            _step.HandleCommand(Qvc.Default.HandleCommand).Serialize(r =>
            {
                r.ShouldBeOfType(typeof(CommandResult));
                r.Success.ShouldBe(false);
                r.Valid.ShouldBe(true);
                r.Exception.ShouldBeOfType(typeof(NullReferenceException));
                return "";
            });
        }
    }
}