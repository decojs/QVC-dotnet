using System;
using System.Threading.Tasks;

using NSubstitute;

using NUnit.Framework;

using Qvc;

using Shouldly;

namespace Tests.TaskExtensions
{
    [TestFixture]
    public class TaskTests
    {
        [Test]
        public async Task TestThen()
        {
            var result = await Task.FromResult("hello")
                .Then(r =>
                {
                    r.ShouldBe("hello");
                    return "hi";
                });

            result.ShouldBe("hi");
        }

        [Test]
        public async Task TestAsyncThen()
        {
            var result = await Task.FromResult("hello")
                .Then(r =>
                {
                    r.ShouldBe("hello");
                    return Task.FromResult("hi");
                });

            result.ShouldBe("hi");
        }

        [Test]
        public void TestSkipThenOnException()
        {
            var spy = Substitute.For<Func<string, string>>();
            var result = Task.Run(() => ThrowException())
                .Then(spy);

            Should.Throw<Exception>(async () => await result).Message.ShouldBe("oops");

            spy.DidNotReceive().Invoke(Arg.Any<string>());
        }

        [Test]
        public void TestThenThrows()
        {
            var spy = Substitute.For<Func<string, string>>();
            var result = Task.FromResult("hello")
                .Then(r => ThrowException())
                .Then(spy);

            Should.Throw<Exception>(async () => await result).Message.ShouldBe("oops");

            spy.DidNotReceive().Invoke(Arg.Any<string>());
        }

        [Test]
        public async Task TestCatchThrows()
        {
            var spy = Substitute.For<Func<string, string>>();
            var result = await Task.FromResult("hello")
                .Then(r => ThrowException())
                .Then(spy)
                .Catch(e =>
                {
                    e.Message.ShouldBe("oops");
                    return "fixed";
                });

            result.ShouldBe("fixed");

            spy.DidNotReceive().Invoke(Arg.Any<string>());
        }

        private static string ThrowException()
        {
            throw new Exception("oops");
        }
    }
}