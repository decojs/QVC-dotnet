using System;
using NUnit.Framework;
using Qvc.Exceptions;
using Qvc.Executables;
using Qvc.Repository.Tree;
using Shouldly;

namespace Tests.Repository.Tree
{
    public class SuffixTreeTest
    {
        private readonly Type _value = typeof(TestCommand);

        [Test]
        public void OneLevelTree_OneLevelMatch()
        {
            var tree = new SuffixTree();
            tree.Add("first", _value);
            tree.Draw();

            tree.Find("first").ShouldBe(_value);
        }

        [Test]
        public void TwoLevelTree_TwoLevelMatch()
        {
            var tree = new SuffixTree();
            tree.Add("first.second", _value);
            tree.Draw();

            tree.Find("first.second").ShouldBe(_value);
        }

        [Test]
        public void TwoLevelTree_OneLevelMatch()
        {
            var tree = new SuffixTree();
            tree.Add("first.second", _value);
            tree.Draw();

            tree.Find("second").ShouldBe(_value);
        }

        [Test]
        public void DuplicateExecutable()
        {
            var tree = new SuffixTree();
            tree.Add("first.second", _value);
            
            Should.Throw<DuplicateExecutableException>(() => tree.Add("first.second", _value))
                .Message.ShouldBe("Cannot have two Executables with the same Name: first.second");

            tree.Draw();
        }

        [Test]
        public void TwoExecutablesInDifferentPackagesFullPath()
        {
            var tree = new SuffixTree();
            tree.Add("first.second.Executable", _value);
            tree.Add("first.third.Executable", _value);
            tree.Draw();
            
            tree.Find("first.second.Executable").ShouldBe(_value);
        }

        [Test]
        public void TwoExecutablesInDifferentPackagesUniquePath()
        {
            var tree = new SuffixTree();
            tree.Add("first.second.Executable", _value);
            tree.Add("first.third.Executable", _value);
            tree.Draw();

            tree.Find("second.Executable").ShouldBe(_value);
        }

        [Test]
        public void TwoExecutablesInDifferentPackagesDuplicatePath()
        {
            var tree = new SuffixTree();
            tree.Add("first.second.Executable", _value);
            tree.Add("first.third.Executable", _value);
            tree.Draw();

            Should.Throw<DuplicateExecutableException>(() => tree.Find("Executable"))
                .Message.ShouldBe("Multiple Executables with that Name, use first.second.Executable or first.third.Executable");
        }

        [Test]
        public void DuplicatePackage_UniqueExecutable()
        {
            var tree = new SuffixTree();
            tree.Add("first.second.second", _value);
            tree.Add("first.second.third", _value);
            tree.Draw();

            tree.Find("second.second").ShouldBe(_value);
        }

        private class TestCommand : ICommand
        {
            
        }
    }
}