using System.Linq;
using NUnit.Framework;
using Shouldly;
using Tests.Executables;
using Tests.Repository;

namespace Tests.Reflection
{
    [TestFixture]
    public class ReflectionTest
    {
        [Test]
        public void TestReflectionOfCommand()
        {
            var handlers = Qvc.Reflection.Reflection.FindCommandHandlers().ToList();
            handlers.ShouldContain(typeof(CommandHandlerA));
            handlers.ShouldContain(typeof(CommandHandlerB));
        }

        [Test]
        public void TestReflectionOfQuery()
        {
            var handlers = Qvc.Reflection.Reflection.FindQueryHandlers().ToList();
            handlers.ShouldContain(typeof(QueryHandlerA));
            handlers.ShouldContain(typeof(QueryHandlerB));
        }

        [Test]
        public void TestReflectionOfCommandHandler()
        {
            Qvc.Reflection.Reflection.GetCommandHandledByHandler(typeof(CommandHandlerA)).ShouldBe(typeof(CommandA));
        }

        [Test]
        public void TestReflectionOfQueryHandler()
        {
            Qvc.Reflection.Reflection.GetQueryHandledByHandler(typeof(QueryHandlerA)).ShouldBe(typeof(QueryA));
        }
    }
}