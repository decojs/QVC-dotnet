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
    public class DontExecuteCommandStepTest
    {
        private IExecuteCommandStep _step;
        private Exception _exception;

        [SetUp]
        public void Setup()
        {
            _exception = new Exception("blabla");
            _step = new DontExecuteCommandStep(_exception);
        }

        [Test]
        public void Test()
        {
            var spy = Substitute.For<Action<IHandleExecutable, ICommand>>();
            _step.HandleCommand(spy).Serialize(r =>
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