using NUnit.Framework;
using Qvc.Executables;
using Qvc.Handlers;
using Qvc.Steps;
using Shouldly;
using Tests.Executables;
using Tests.Repository;

namespace Tests.Steps
{
    [TestFixture]
    public class ExecuteCommandStepTest
    {
        private ExecuteCommandStep _step;
        private ICommand _command;
        private IHandleExecutable _handler;

        [SetUp]
        public void Setup()
        {
            _command = new CommandB();
            _handler = new CommandHandlerB();
            _step = new ExecuteCommandStep(_command, _handler);
        }

        [Test]
        public void Test()
        {
            _step.HandleCommand((h, c) =>
            {
                h.ShouldBe(_handler);
                c.ShouldBe(_command);
            });
        }
    }
}