using System;
using System.Collections.Generic;
using System.Reflection;

using NUnit.Framework;
using Shouldly;
using ReflectionTests.TestMaterial;

namespace ReflectionTests.Reflection
{
    [TestFixture]
    public class ReflectionTest
    {
        private IEnumerable<Type> _types;

        [SetUp]
        public void Setup()
        {
            _types = Assembly.GetAssembly(typeof(ReflectionTest)).GetTypes();
        }

        [Test]
        public void TestReflectionOfCommand()
        {
            var handlers = Qvc.Reflection.Reflection.FindCommandHandlers(_types);
            handlers.Count.ShouldBe(3);
            handlers.ShouldContain(typeof(CommandHandlerA));
            handlers.ShouldContain(typeof(CommandHandlerB));
            handlers.ShouldContain(typeof(MultipleHandler));
        }

        [Test]
        public void TestReflectionOfQuery()
        {
            var handlers = Qvc.Reflection.Reflection.FindQueryHandlers(_types);
            handlers.Count.ShouldBe(3);
            handlers.ShouldContain(typeof(QueryHandlerA));
            handlers.ShouldContain(typeof(QueryHandlerB));
            handlers.ShouldContain(typeof(MultipleHandler));
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

        [Test]
        public void TestReflectionOfMultipleQueryHandler()
        {
            var queries = Qvc.Reflection.Reflection.GetQueriesHandledByHandler(typeof(MultipleHandler));
            queries.Count.ShouldBe(2);
            queries.ShouldContain(typeof(QueryC));
            queries.ShouldContain(typeof(QueryD));
        }

        [Test]
        public void TestReflectionOfMultipleCommandHandler()
        {
            var queries = Qvc.Reflection.Reflection.GetCommandsHandledByHandler(typeof(MultipleHandler));
            queries.Count.ShouldBe(2);
            queries.ShouldContain(typeof(CommandC));
            queries.ShouldContain(typeof(CommandD));
        }
    }
}