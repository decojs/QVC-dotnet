using System;
using NUnit.Framework;
using Qvc;
using Qvc.Executables;
using Qvc.Handlers;
using Qvc.Steps;
using Shouldly;
using Tests.Executables;

namespace Tests.Steps
{
    [TestFixture]
    public class ExecuteCommandStepTest
    {
        private CommandAndHandler _step;
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
            var result = CommandSteps.HandleCommand(_step, async (h, c) =>
            {
                h.ShouldBe(_handler);
                c.ShouldBe(_command);
            });

            result.Success.ShouldBe(true);
            result.Valid.ShouldBe(true);
            result.Exception.ShouldBe(null);
        }

        [Test]
        public void TestWhenThrows()
        {
            Should.Throw<NullReferenceException>(() =>
                CommandSteps.HandleCommand(_step, (h, c) =>
                {
                    throw new NullReferenceException();
                }));
        }
    }
}