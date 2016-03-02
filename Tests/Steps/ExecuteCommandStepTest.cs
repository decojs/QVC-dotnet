using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Qvc.Executables;
using Qvc.Handlers;
using Qvc.Steps;
using Shouldly;

using Tests.TestMaterial;

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
        public async Task Test()
        {
            var result = await CommandSteps.HandleCommand(_step, async (h, c) =>
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

        [Test]
        public void TestWhenThrowsAsync()
        {
            Should.Throw<NullReferenceException>(() =>
                CommandSteps.HandleCommand(_step, async (h, c) =>
                {
                    throw new NullReferenceException();
                }));
        }
    }
}