﻿using NUnit.Framework;
using Qvc;
using Qvc.Exceptions;
using Qvc.Results;
using Qvc.Steps;
using Shouldly;
using Tests.Executables;

namespace Tests.Steps
{
    [TestFixture]
    public class QueryNameAndJsonTest
    {
        private QueryNameAndJson _step;

        [SetUp]
        public void Setup()
        {
            _step = new QueryNameAndJson("name", "json");
        }

        [Test]
        public void TestQuery()
        {
            _step.FindQuery(name =>
            {
                name.ShouldBe("name");
                return typeof(QueryA);
            });
        }

        [Test]
        public void TestQueryDoesNotExist()
        {
            _step.FindQuery(name =>
            {
                throw new QueryDoesNotExistException(name);
            }).ShouldBeOfType<QueryResult>();
        }

        [Test]
        public void TestQueryExecutableDoesNotExist()
        {
            _step.FindQuery(name =>
            {
                throw new ExecutableDoesNotExistException(name);
            }).ShouldBeOfType<QueryResult>();
        }
    }
}