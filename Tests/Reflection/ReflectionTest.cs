using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Shouldly;
using Tests.Executables;

namespace Tests.Reflection
{
    [TestFixture]
    public class ReflectionTest
    {
        private IEnumerable<Type> _types;

        [SetUp]
        public void Setup()
        {
            _types = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes());
        }

        [Test]
        public void TestReflectionOfCommand()
        {
            var handlers = Qvc.Reflection.Reflection.FindCommandHandlers(_types).ToList();
            handlers.ShouldContain(typeof(CommandHandlerA));
            handlers.ShouldContain(typeof(CommandHandlerB));
        }

        [Test]
        public void TestReflectionOfQuery()
        {
            var handlers = Qvc.Reflection.Reflection.FindQueryHandlers(_types).ToList();
            handlers.ShouldContain(typeof(QueryHandlerA));
            handlers.ShouldContain(typeof(QueryHandlerB));
        }

        [Test]
        public void TestReflectionOfCommandHandler()
        {
            Qvc.Reflection.Reflection.GetCommandsHandledByHandler(typeof(CommandHandlerA))
                .ShouldContain(typeof(CommandA));
        }

        [Test]
        public void TestReflectionOfQueryHandler()
        {
            Qvc.Reflection.Reflection.GetQueriesHandledByHandler(typeof(QueryHandlerA))
                .ShouldContain(typeof(QueryA));
        }
    }
}