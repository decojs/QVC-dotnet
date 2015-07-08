using System.Collections.Generic;
using System.Reflection;

using NUnit.Framework;
using Qvc;
using Qvc.Constraints;
using Qvc.Repository;
using Qvc.Results;
using Qvc.Steps;

using Shouldly;

namespace Tests
{
    [TestFixture]
    public class FullTest
    {
        private ExecutableRepository _repo;
        private HandlerRepository _handlerRepo;

        [SetUp]
        public void Setup()
        {
            var types = Assembly.GetAssembly(typeof(FullTest)).GetTypes();
            _repo = new ExecutableRepository();
            _handlerRepo = new HandlerRepository();
            Qvc.Reflection.Setup.SetupRepositories(_handlerRepo, _repo, types);
        }

        [Test]
        public async void ExecuteCommand()
        {
            var result = await Action.Command("CommandFullTest", "{}")
                .ThenFindCommand(_repo.FindCommand)
                .ThenDeserializeCommand()
                .ThenValidateCommand()
                .ThenFindCommandHandler(_handlerRepo.FindCommandHandler)
                .ThenCreateCommandHandler()
                .ThenHandleCommand()
                .ThenSerializeResult();
            result.ShouldBe("{\"success\":true,\"valid\":true,\"exception\":null,\"violations\":[]}");
        }

        [Test]
        public async void ExecuteQuery()
        {
            var result = await Action.Query("QueryFullTest", "{}")
                .ThenFindQuery(_repo.FindQuery)
                .ThenDeserializeQuery()
                .ThenValidateQuery()
                .ThenFindQueryHandler(_handlerRepo.FindQueryHandler)
                .ThenCreateQueryHandler()
                .ThenHandleQuery()
                .ThenSerializeResult();
            result.ShouldBe("{\"result\":\"hello\",\"success\":true,\"valid\":true,\"exception\":null,\"violations\":[]}");
        }

        [Test]
        public async void ExecuteCommandThatThrows()
        {
            var result = await Action.Command("CommandThatThrows", "{}")
                .ThenFindCommand(_repo.FindCommand)
                .ThenDeserializeCommand()
                .ThenValidateCommand()
                .ThenFindCommandHandler(_handlerRepo.FindCommandHandler)
                .ThenCreateCommandHandler()
                .ThenHandleCommand()
                .Catch(CommandSteps.ExceptionToCommandResultDev)
                .ThenSerializeResult();
            result.ShouldStartWith("{\"success\":false,\"valid\":true,\"exception\":{\"ClassName\":\"System.NotImplementedException");
        }

        [Test]
        public async void ExecuteQueryThatThrows()
        {
            var result = await Action.Query("QueryThatThrows", "{}")
                .ThenFindQuery(_repo.FindQuery)
                .ThenDeserializeQuery()
                .ThenValidateQuery()
                .ThenFindQueryHandler(_handlerRepo.FindQueryHandler)
                .ThenCreateQueryHandler()
                .ThenHandleQuery()
                .Catch(QuerySteps.ExceptionToQueryResultDev)
                .ThenSerializeResult();
            result.ShouldStartWith("{\"result\":null,\"success\":false,\"valid\":true,\"exception\":{\"ClassName\":\"System.NotImplementedException");
        }

        [Test]
        public async void ExecuteCommandThatThrowsInDev()
        {
            var result = await Action.Command("CommandThatThrows", "{}")
                .ThenFindCommand(_repo.FindCommand)
                .ThenDeserializeCommand()
                .ThenValidateCommand()
                .ThenFindCommandHandler(_handlerRepo.FindCommandHandler)
                .ThenCreateCommandHandler()
                .ThenHandleCommand()
                .ThenSerializeResult();
            result.ShouldStartWith("{\"success\":false,\"valid\":true,\"exception\":null,\"violations\":[]}");
        }

        [Test]
        public async void ExecuteQueryThatThrowsInDev()
        {
            var result = await Action.Query("QueryThatThrows", "{}")
                .ThenFindQuery(_repo.FindQuery)
                .ThenDeserializeQuery()
                .ThenValidateQuery()
                .ThenFindQueryHandler(_handlerRepo.FindQueryHandler)
                .ThenCreateQueryHandler()
                .ThenHandleQuery()
                .ThenSerializeResult();
            result.ShouldStartWith("{\"result\":null,\"success\":false,\"valid\":true,\"exception\":null,\"violations\":[]}");
        }

        [Test]
        public async void ExecuteCommandThatThrowsValidationException()
        {
            var result = await Action.Command("CommandThatThrowsValidationException", "{}")
                .ThenFindCommand(_repo.FindCommand)
                .ThenDeserializeCommand()
                .ThenValidateCommand()
                .ThenFindCommandHandler(_handlerRepo.FindCommandHandler)
                .ThenCreateCommandHandler()
                .ThenHandleCommand()
                .ThenSerializeResult();
            result.ShouldStartWith("{\"success\":false,\"valid\":false,\"exception\":null,\"violations\":[{\"fieldName\":\"\",\"message\":\"oops\"}]");
        }

        [Test]
        public async void ExecuteQueryThatThrowsValidationException()
        {
            var result = await Action.Query("QueryThatThrowsValidationException", "{}")
                .ThenFindQuery(_repo.FindQuery)
                .ThenDeserializeQuery()
                .ThenValidateQuery()
                .ThenFindQueryHandler(_handlerRepo.FindQueryHandler)
                .ThenCreateQueryHandler()
                .ThenHandleQuery()
                .ThenSerializeResult();
            result.ShouldStartWith("{\"result\":null,\"success\":false,\"valid\":false,\"exception\":null,\"violations\":[{\"fieldName\":\"\",\"message\":\"oops\"}]");
        }

        [Test]
        public async void GetConstraints()
        {
            var result = await Action.Constraints("QueryFullTest")
                .ThenFindExecutable(_repo.FindExecutable)
                .ThenGetConstraints(type => new ConstraintsResult(new List<Parameter>()))
                .ThenSerialize();
            result.ShouldBe("{\"parameters\":[]}");
        }
    }
}