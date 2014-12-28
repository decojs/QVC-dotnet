using System;
using System.Collections.Generic;
using NUnit.Framework;
using Qvc.Exceptions;
using Qvc.Repository;
using Shouldly;
using Tests.Executables;

namespace Tests.Repository
{
    [TestFixture]
    public class ExecutableRepositoryTest
    {
        private ExecutableRepository _repo;

        [SetUp]
        public void Setup()
        {
            _repo = new ExecutableRepository();
            _repo.AddExecutables(new List<Type>()
            {
                typeof(CommandA),
                typeof(CommandB),
                typeof(CommandC),
                typeof(QueryA),
                typeof(QueryB),
                typeof(QueryC),
            });
        }

        [Test]
        public void FindExecutableCommand()
        {
            _repo.FindExecutable("CommandA").ShouldBe(typeof(CommandA));
        }

        [Test]
        public void FindExecutableQuery()
        {
            _repo.FindExecutable("QueryA").ShouldBe(typeof(QueryA));
        }

        [Test]
        public void FindExecutableThatDoesntExist()
        {
            Should.Throw<ExecutableDoesNotExistException>(() => _repo.FindExecutable("QueryFullTest"));
        }

        [Test]
        public void FindCommandCommand()
        {
            _repo.FindCommand("CommandA").ShouldBe(typeof(CommandA));
        }

        [Test]
        public void FindQueryQuery()
        {
            _repo.FindQuery("QueryA").ShouldBe(typeof(QueryA));
        }

        [Test]
        public void FindCommandThatDoesntExist()
        {
            Should.Throw<CommandDoesNotExistException>(() => _repo.FindCommand("CommandFullTest"));
        }

        [Test]
        public void FindQueryThatDoesntExist()
        {
            Should.Throw<QueryDoesNotExistException>(() => _repo.FindQuery("QueryFullTest"));
        }

        [Test]
        public void FindQueryCommand()
        {
            Should.Throw<QueryDoesNotExistException>(() => _repo.FindQuery("CommandA"));
        }

        [Test]
        public void FindCommandQuery()
        {
            Should.Throw<CommandDoesNotExistException>(() => _repo.FindCommand("QueryA"));
        }
    }
}